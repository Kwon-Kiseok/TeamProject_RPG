using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HOGUS.Scripts.Manager;

public struct st_ObstacleRendererInfo
{
    public int InstanceID;
    public MeshRenderer meshRenderer;
    public Shader originShader;
}

public class SeeThroughObstacles : MonoBehaviour, IUpdatableObject
{
    public CamFollow mainFollowCam;

    private Dictionary<int, st_ObstacleRendererInfo> dicObstacles = new Dictionary<int, st_ObstacleRendererInfo>();
    private List<st_ObstacleRendererInfo> listTransparentedRenderer = new();
    private Color TransparentColor = new Color(1f, 1f, 1f, 0.2f);
    private Color OriginColor = new Color(1f, 1f, 1f, 1f);
    private string sharderColorParamName = "_Color";
    private Shader transparentShader;
    private RaycastHit[] transparentedRaycasts;
    private LayerMask transparentedLayer;


    public float Distance;

    public void OnDisable()
    {
        if(UpdateManager.Instance != null)
            UpdateManager.Instance.DeregisterUpdatableObject(this);
    }

    public void OnEnable()
    {
        UpdateManager.Instance.RegisterUpdatableObject(this);
        Init();
    }

    public void OnFixedUpdate(float deltaTime)
    {
        
    }

    public void OnUpdate(float deltaTime)
    {
        TransparentProcess();
    }

    private void Init()
    {
        // layermask가 Obstacle로 설정된 오브젝트만 감지
        transparentedLayer = 1 << LayerMask.NameToLayer("Obstacle");
        // 반투명쉐이더로 사용할 기본 transparent 쉐이더
        transparentShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
    }

    private void TransparentProcess()
    {
        if (mainFollowCam.target == null) 
            return;

        // 반투명 됐던 오브젝트들을 담은 리스트를 검사하여 원래 쉐이더로 돌려줌
        if(listTransparentedRenderer.Count > 0)
        {
            for(int i = 0; i < listTransparentedRenderer.Count; ++i)
            {
                listTransparentedRenderer[i].meshRenderer.material.shader = listTransparentedRenderer[i].originShader;
            }

            listTransparentedRenderer.Clear();
        }

        Vector3 targetPos = mainFollowCam.target.position;
        Distance = (mainFollowCam.transform.position - targetPos).magnitude;
        
        Vector3 DirToCam = (mainFollowCam.transform.position - targetPos).normalized;

        HitRayTransparentObject(targetPos, DirToCam, Distance);
    }

    private void HitRayTransparentObject(Vector3 start, Vector3 direction, float distance)
    {
        // 카메라 - 캐릭터 간의 방향으로 지정된 레이어마스크의 hit된 레이캐스트들을 담아둠
        transparentedRaycasts = Physics.RaycastAll(start, direction, distance, transparentedLayer);

        for (int i = 0; i < transparentedRaycasts.Length; ++i)
        {
            int instanceID = transparentedRaycasts[i].colliderInstanceID;

            // dictionary에 담겨있는지 검사 (매 프레임 중복 검사하는 비용 감소)
            if (!dicObstacles.ContainsKey(instanceID))
            {
                MeshRenderer obsRenderer = transparentedRaycasts[i].collider.gameObject.GetComponent<MeshRenderer>();
                st_ObstacleRendererInfo rendererInfo = new st_ObstacleRendererInfo();
                rendererInfo.InstanceID = instanceID;
                rendererInfo.meshRenderer = obsRenderer;
                rendererInfo.originShader = obsRenderer.material.shader;

                dicObstacles[instanceID] = rendererInfo;
            }

            // 쉐이더를 설정해둔 반투명 쉐이더로 변경
            dicObstacles[instanceID].meshRenderer.material.shader = transparentShader;
            // 알파값을 줄인 칼라값으로 쉐이더 색 변경
            dicObstacles[instanceID].meshRenderer.material.SetColor(sharderColorParamName, TransparentColor);

            listTransparentedRenderer.Add(dicObstacles[instanceID]);
        }
    }
}

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RenderCamera : MonoBehaviour {

    public Shader shader;
    [Header("Center of the universe")]
    public Vector3 fixedCenterOfUniverse;
    public Transform objectAtCenterOfUniverse;
    [Header("Universe configuration")]
    public float universeRadius = 2f;
    public float featheringRadius = 1f;
    public Color chromaKey = new Color(0, 1, 0, 0.618f);
    public bool clipBelowGround = true;
    [Header("Background")]
    public Texture backgroundTexture;
    public MonoBehaviour backgroundTextureProvider;

    Material material;
    new Camera camera;

    void Update() {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (material == null) {
            material = new Material(shader);
            material.hideFlags = HideFlags.DontSave;
            camera = GetComponent<Camera>();
            camera.backgroundColor = chromaKey;
        }

        material.SetFloat("_UniverseRadius", universeRadius);
        material.SetFloat("_FeatheringRadius", featheringRadius);
        material.SetColor("_ChromaKey", chromaKey);
        material.SetInt("_ClipBelowGround", clipBelowGround ? 1 : 0);

        var matrix = camera.cameraToWorldMatrix;
        material.SetMatrix("_InverseView", matrix);

        Vector3 centerPosition = objectAtCenterOfUniverse != null ? objectAtCenterOfUniverse.position : fixedCenterOfUniverse;
        material.SetVector("_CenterOfUniverse", centerPosition);

        Texture background = null;
        if (backgroundTexture != null)
            background = backgroundTexture;
        else if (Application.isPlaying && backgroundTextureProvider != null) {
            var provider = backgroundTextureProvider as IBackgroundTextureProvider;
            Debug.Assert(provider != null, "BackgroundTextureProvider needs to be an IBackgroundTextureProvider");
            background = provider.GetBackgroundTexture();
        }
        material.SetTexture("_BackgroundTex", background);

        Graphics.Blit(source, destination, material);
    }

    public interface IBackgroundTextureProvider {
        Texture GetBackgroundTexture();
    }
}
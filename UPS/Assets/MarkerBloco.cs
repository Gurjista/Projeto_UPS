using UnityEngine;

public class MarkerBloco : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform; // referência para a transform da câmera
    [SerializeField] private float _distanceThreshold = 10f; // distância a partir da qual o marcador é exibido
    [SerializeField] private float _maxSize = 5f; // tamanho máximo do marcador quando visto de perto
    [SerializeField] private float _minSize = 0.5f; // tamanho mínimo do marcador quando visto de longe
    [SerializeField] private Texture2D _markerTexture; // textura do marcador
    //public float zoomSensitivity = 0.01f;

    private Renderer _renderer;
    private Material _material;
    //[SerializeField] private bool isBlock;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _material = _renderer.material;

        // Configura o material com a textura do marcador
        _material.mainTexture = _markerTexture;
    }

    private void Update()
    {
        // Verifica a distância da câmera para este marcador
        float distance = Vector3.Distance(transform.position, _cameraTransform.position);

        // Verifica se o marcador deve ser exibido
        if (distance < _distanceThreshold)
        {
            _renderer.enabled = false; // esconde o marcador
            return;
        }
        
        _renderer.enabled = true; // exibe o marcador
        
        

        // Calcula o tamanho do marcador com base na distância da câmera
        float size = Mathf.Lerp(_maxSize, _minSize, distance / _distanceThreshold);

        // Define o tamanho do marcador
        transform.localScale = new Vector3(size * 6, size * 6, size * 6);

        // Rotaciona o marcador na direção da câmera
        //transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);

        transform.LookAt( _cameraTransform.position);


        transform.Rotate(90, 90, 90, Space.Self);

    }
}

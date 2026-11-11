/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Singleton del menú contextual persistente que sobrevive a los cambios de escena.
 * Explicación: Garantiza que exista una única instancia del menú: si al volver a la escena Inicio
 *              ya hay una creada, destruye la copia nueva. Marca el objeto con DontDestroyOnLoad y
 *              elimina los EventSystem sobrantes que traigan las demás escenas.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Se ejecuta antes que el resto de los scripts para poder descartar duplicados a tiempo.
[DefaultExecutionOrder(-100)]
public class MenuPersistente : MonoBehaviour
{
    public static MenuPersistente Instancia { get; private set; }

    [Tooltip("EventSystem que vive dentro del menú persistente. Si queda vacío se busca entre los hijos.")]
    [SerializeField] private EventSystem eventSystemPropio;

    private void Awake()
    {
        // Si ya existe un menú (por ejemplo, al volver a la escena Inicio), esta copia sobra.
        if (Instancia != null && Instancia != this)
        {
            // Se desactiva antes de destruirla para que sus hijos no ejecuten Awake/OnEnable/Start.
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        Instancia = this;

        // DontDestroyOnLoad solo funciona con objetos raíz: este GameObject no debe tener padre.
        DontDestroyOnLoad(gameObject);

        if (eventSystemPropio == null)
        {
            eventSystemPropio = GetComponentInChildren<EventSystem>(true);
        }

        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDestroy()
    {
        if (Instancia == this)
        {
            SceneManager.sceneLoaded -= AlCargarEscena;
            Instancia = null;
        }
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        EliminarEventSystemsAjenos();
    }

    // Unity solo admite un EventSystem activo: se conserva el del menú y se quitan los de las escenas.
    private void EliminarEventSystemsAjenos()
    {
        if (eventSystemPropio == null) return;

        EventSystem[] todos = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        foreach (EventSystem es in todos)
        {
            if (es != eventSystemPropio)
            {
                Destroy(es.gameObject);
            }
        }
    }
}
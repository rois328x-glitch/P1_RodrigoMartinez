El planteamiento principal fue crear un control que se sintiera fluido y moderno (estilo teclado y ratón).

Movimiento Relativo a la Cámara: En lugar de usar coordenadas globales, el movimiento se calcula basándose en la orientación de la cameraTransform. Esto permite que el jugador avance siempre hacia donde apunta la vista, mejorando la experiencia de juego en 3D.

Sistema de Sprint (Opcional): Se implementó una lógica de aumento de velocidad temporal mediante la tecla LeftShift, multiplicando la velocidad base por un sprintMultiplier.

Salto y Doble Salto (Opcional): Se configuró un sistema de salto basado en fuerzas físicas (AddForce con Impulse). Además, se incluyó la opción de Doble Salto, gestionada mediante el booleano hasDoubleJumped que se resetea al detectar el suelo mediante Tags.

Físicas y Rotación: Se bloquearon las rotaciones en X y Z en el Rigidbody para evitar que el personaje vuelque, y se programó una rotación visual suave hacia la dirección del movimiento para mayor realismo.

Sistema de Cámara y Vista (MouseLook)
Se optó por una cámara en tercera persona controlada por el ratón.

Control de Ejes: El script MouseLook captura el movimiento del ratón para rotar el cuerpo del jugador horizontalmente y la cámara verticalmente.

Limitación de Ángulo: Se aplicó un Mathf.Clamp a la rotación vertical para evitar que la cámara dé la vuelta completa y desoriente al jugador.

Mecánicas de Plataformas
Se desarrollaron los dos tipos de plataformas solicitados con lógicas específicas:

Plataforma Móvil (Tipo A): Se utilizó rb.MovePosition en el FixedUpdate para asegurar un movimiento físico constante entre dos puntos. Para resolver el problema de que el jugador se resbalara al moverse la base, se implementó un sistema de Parentesco Dinámico (SetParent) en el OnCollisionEnter, haciendo que el jugador sea "hijo" de la plataforma mientras esté sobre ella.

Plataforma de Caída (Tipo B): Esta plataforma utiliza Time.time para gestionar una cuenta atrás tras el primer contacto con el jugador. Una vez cae hasta una alturaMinima, inicia una Corrutina para esperar y regresar suavemente a su posición original, permitiendo la rejugabilidad del nivel.


Práctica 2: Bucle de Juego, UI y Sistemas Avanzados

Patrones de Diseño y GameManager
El núcleo de esta actualización es el GameManagerClass, encargado de almacenar el estado global del juego.


Patrón Singleton: Se implementó para asegurar que solo exista una instancia del GameManager, facilitando el acceso desde cualquier script sin acoplamientos innecesarios.


Patrón Observer (Eventos): En lugar de actualizar la interfaz directamente, el GameManager lanza eventos (Action) cada vez que el número de monedas o vidas cambia. El script UIUpdater se suscribe a estos eventos, manteniendo la lógica del juego completamente separada de la lógica visual.

Sistemas de Coleccionables y PeligrosSistema de Monedas: Se crearon objetos coleccionables que el jugador puede recoger. Al detectar la colisión mediante OnTriggerEnter, el script notifica al GameManager y destruye el objeto de la escena.Sistema de Vidas y Daño: Se integró un sistema de vidas gestionado por el GameManager. Se crearon zonas de daño (SpikeBalls) que actúan como Triggers. Al entrar en contacto con el jugador, estas zonas llaman al método para restar una vida.Sistema de Respawn: Al perder una vida, el jugador es teletransportado inmediatamente a una posición inicial segura guardada al inicio del nivel. Para evitar comportamientos erráticos en las físicas, se implementó un reseteo de la velocidad del Rigidbody (linearVelocity y angularVelocity) durante el respawn.Flujo de Juego y Gestión de EscenasSe construyó el flujo completo del juego dividiéndolo en varias escenas configuradas en los Build Settings.Menú Principal: Escena inicial (MainMenu) con interfaz gráfica para iniciar la partida.Meta del Nivel: Un objeto final ("Goal") con un Collider en modo Trigger que detecta la llegada del jugador.Escena de Victoria: Pantalla final con opciones para volver a intentar el nivel o regresar al menú principal. Toda la lógica de SceneManager.LoadScene se centralizó eficientemente dentro del GameManager.

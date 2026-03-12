1. Control del Personaje (PlayerController)
El planteamiento principal fue crear un control que se sintiera fluido y moderno (estilo teclado y ratón).

Movimiento Relativo a la Cámara: En lugar de usar coordenadas globales, el movimiento se calcula basándose en la orientación de la cameraTransform. Esto permite que el jugador avance siempre hacia donde apunta la vista, mejorando la experiencia de juego en 3D.

Sistema de Sprint (Opcional): Se implementó una lógica de aumento de velocidad temporal mediante la tecla LeftShift, multiplicando la velocidad base por un sprintMultiplier.

Salto y Doble Salto (Opcional): Se configuró un sistema de salto basado en fuerzas físicas (AddForce con Impulse). Además, se incluyó la opción de Doble Salto, gestionada mediante el booleano hasDoubleJumped que se resetea al detectar el suelo mediante Tags.

Físicas y Rotación: Se bloquearon las rotaciones en X y Z en el Rigidbody para evitar que el personaje vuelque, y se programó una rotación visual suave hacia la dirección del movimiento para mayor realismo.

2. Sistema de Cámara y Vista (MouseLook)
Se optó por una cámara en tercera persona controlada por el ratón.

Control de Ejes: El script MouseLook captura el movimiento del ratón para rotar el cuerpo del jugador horizontalmente y la cámara verticalmente.

Limitación de Ángulo: Se aplicó un Mathf.Clamp a la rotación vertical para evitar que la cámara dé la vuelta completa y desoriente al jugador.

3. Mecánicas de Plataformas
Se desarrollaron los dos tipos de plataformas solicitados con lógicas específicas:

Plataforma Móvil (Tipo A): Se utilizó rb.MovePosition en el FixedUpdate para asegurar un movimiento físico constante entre dos puntos. Para resolver el problema de que el jugador se resbalara al moverse la base, se implementó un sistema de Parentesco Dinámico (SetParent) en el OnCollisionEnter, haciendo que el jugador sea "hijo" de la plataforma mientras esté sobre ella.

Plataforma de Caída (Tipo B): Esta plataforma utiliza Time.time para gestionar una cuenta atrás tras el primer contacto con el jugador. Una vez cae hasta una alturaMinima, inicia una Corrutina para esperar y regresar suavemente a su posición original, permitiendo la rejugabilidad del nivel.

4. Animaciones y Estética (Opcional)
Se configuró un Animator Controller con una máquina de estados para dar feedback visual al jugador:

Estados: Se crearon transiciones entre Idle, Walk y Jump.

Parámetros dinámicos: El script PlayerController envía en tiempo real los valores de speed (basado en la magnitud del movimiento) y isGrounded al Animator para activar la animación correcta en cada momento.

5. Organización y Control de Versiones
Estructura del Proyecto: Se siguió una jerarquía de carpetas estricta (Models, Scenes, scripts, texturas) para facilitar la escalabilidad del proyecto.

Uso de Git: Se configuró un repositorio en GitHub utilizando un archivo .gitignore específico para Unity, asegurando que solo se suban los archivos necesarios y omitiendo carpetas pesadas como Library.

Extras Destacados:
Regreso de Plataforma: La plataforma tipo B no solo cae, sino que vuelve a su sitio automáticamente tras unos segundos.

Parentesco Inteligente: Solución técnica mediante código para que el jugador no se caiga de las plataformas móviles.

Doble Salto: Funcionalidad añadida para facilitar la navegación en secciones de plataformas difíciles.

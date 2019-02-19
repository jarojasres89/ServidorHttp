# ServidorHttp
Servidor http de prueba

## Objetivo
* Definición inicial de una arquitectura detallada para la creación de un servidor Http
* Utilizar y mejorar las habilidades de análisis detallado de arquitectura de software

## Descripción general
* Para la creación de un servidor Http se debe iniciar un proceso que reciba peticiones a traves de TCP, estas peticiones llevan un mensaje con un formato especifico que debe ser interpretado de acuerdo a los estandares Http. De acuerdo al contenido del mensaje se deben ejecutar acciones específicas que hacen parte de la definición Http, sin embargo, no todas las acciones estan contempladas dentro del alcance de este proyecto.

## Arquitectura
* Basados en la descripción general, se definirá la arquitectura detallada para la solución utilizando los siguientes componentes:
1. Servicio que reciba mensajes a traves de TCP
2. Componente que interpreta el mensaje
3. Componentes que ejecutan cada una de las acciones requeridas por el estandar Http.
4. Componente para retornar código de estado 200 para todas las peticiones
5. Componente para el registro de todas las peticiones en un archivo .log

Los componentes se pueden ver [aquí](Componentes.png)

* El flujo de trabajo del servidor Http es el siguiente:
	1. El servicio se inicia para escuchar las peticiones
	2. Cuando llega una petición, se realiza un registro de log con el contenido de la petición entrante
	3. El contenido de la petición debe ser interpretado.
	4. Se debe revisar en un listado de acciones si la petición cumple con las caracteristicas requeridas para ejecutar una o muchas de estas acciones.
	5. Al finalizar con la ejecucion de las acciones, se debe retornar el mensaje con el código de estado 200.

* El servidor funciona como un controlador, que coordina la ejecución de todo el flujo soportandose en los componentes alternos.

* Para la ejecución de las diferentes acciones, se implementará un listado que permite agregar, borrar y ejecutar las acciones de acuerdo a las condiciones establecidas en cada una de ellas.

* El interprete Http se encarga de traducir el mensaje que ingresa y entrega una solicitud estrucutrada para su procesamiento. También se encarga de traducir una respuesta estructurada a un formato Http.

* El creador de respuesta se implementa de manera independiente debido a que en un principio se debe retornar un código de estado 200, pero esta funcionalidad podria ser modificada en cualquier momento. Implementar la funcionalidad mediante una interface es una forma sencilla de mitigar el riesgo ante cualquier cambio en la respuesta.

* El interprete Http, el creador de respuesta y el escritor de log son componentes externos al servidor que pueden sufrir grandes cambios en sus funcionalidades, por lo tanto, se implementan mediante interfaces para evitar el acoplamiento.

* Las clases y sus funcionalidades se pueden ver en este [diagrama](ClassDiagram.png)

## Herramientas de desarrollo
* El lenguaje a utilizar en el proceso de construcción del servidor Http será C#. 
* La plataforma .Net y en especial el lenguaje de programación C# es bien conocido por dos de los tres miembros del equipo y ellos servirán de soporte para mejorar el proceso de aprendizaje del ultimo integrante. 
* La herramienta seleccionada permite implementar cada una de las funcionalidades necesarias para la creación del servidor Http
* Se cuenta con suficiente documentación para crear el servidor Http con tecnología .Net.

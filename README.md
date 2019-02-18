# ServidorHttp
Servidor http de prueba

## Objetivo
* Definición inicial de una arquitectura detallada para la creación de un servidor Http
* Utilizar y mejorar las habilidades de análisis detallado de arquitectura de software


## Descripción general
* Para la creación de un servidor Http se debe iniciar un proceso que reciba peticiones a travez de TCP, estas peticiones llevan un mensaje con un formato especifico que debe ser interpretado de acuerdo a los estandares Http. De acuerdo al contenido del mensaje se deben ejecutar acciones específicas que hacen parte de la definición Http, sin embargo, no todas las acciones estan contempladas dentro del alcance de este proyecto.

## Arquitectura
* Basados en la descripción general, se definirá la arquitectura detallada para los siguientes componentes:
1. Servicio que reciba mensajes a travez de TCP
2. Componente que interpreta el mensaje
3. Componentes que ejecutan cada una de las acciones requeridas por el estandar Http.
4. Componente para retornar código de estado 200 para todas las peticiones
5. Componente para el registro de todas las peticiones en un archivo .log


## Herramientas de desarrollo
* El lenguaje a utilizar en el proceso de construcción del servidor Http será C#. 
* El framework de .Net y en especial el lenguaje de programación C# es bien conocido por dos de los tres miembros del equipo y ellos servirán de soporte para mejorar el proceso de aprendizaje del ultimo integrante. 
* La herramienta seleccionada permite implementar cada una de las funcionalidades necesarias para la creación del servidor Http
* Se cuenta con suficiente documentación para crear el servidor Http con tecnología .Net.

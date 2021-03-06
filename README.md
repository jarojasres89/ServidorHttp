# Contenido

- [Servidor HTTP](#serv)
  * [OSI](#osi)
  * [HTTP](#http)
  * [Flujo HTTP](#flujo)
  * [Mensajes HTTP](#msg)
  * [Peticiones](#req)
  * [Respuestas](#res)  
- [Objetivo](#obj)
- [Descripción general](#desc)
- [Arquitectura](#arc)
- [Herramientas de desarrollo](#tool)
- [Instrucciones de uso](#use)
- [Pruebas](#test)
  * [Visual Studio](#vs)
  * [Postman](#pm)
- [Screencast](#screen)

<a name="serv"></a>
# Servidor HTTP

## ¿Por dónde empezar?

<a name="osi"></a>
### OSI

El modelo de interconexión de sistemas abiertos (Open System Interconnection) es un modelo conceptual que caracteriza y estandariza las funciones de comunicación de un sistema de telecomunicaciones sin tener en cuenta la estructura interna y la tecnologías subyancentes.
Su objetivo es la interoperabilidad de diversos sistemas de comunicación con protocolos estándar. El modelo divide un sistema de comunicación en capas de abstracción. La versión original del modelo está compuesto por 7 capas.

<a name="http"></a>
### HTTP

De **RFC2616**: El protocolo de transferencia de hipertexto (HTTP), es un protocolo genérico, sin estado, que puede usarse para muchas tareas más allá de su uso para el hipertexto, como los servidores de nombres y sistemas de gestión de objetos distribuidos, a través de la extensión de sus métodos de petición, códigos de error y encabezados. Una caracteristica de HTTP es la tipificación y negociación de representación de datos, permitiendo la construcción de sistemas independiente de los datos que se transfieren.

La comunicación HTTP por lo general se lleva acabo a través de conexiones TCP/IP. El puerto predeterminado es TCP 80, pero se pueden usar otros puertos.

La primera versión de HTTP (HTTP/0.9) era un protocolo simplete para la transferencia de datos en bruto (raw data) a través de internet. El HTTP/1.0 permitía que los mensajes tuvieran un formato de tipo MIME, los cuales contienen metadata sobre los datos transferidos, además de modificadores sobre la petición / respuesta. Sin embargo, HTTP/1.0 no toma suficientemente en cuenta los efectos de la jerarquía de proxies, almacenamiento de caché, la necesidad de conexiones persistentes, entre otros.

HTTP/1.1 incluye requisitos más estrictos que HTTP/1.0 para asegurar la implementación confiable de sus características.

<a name="flujo"></a>
### Flujo HTTP

Cuando el cliente quiere comunicarse con el servidor, tanto si es directamente con él, o a través de un proxy intermedio, realiza los siguientes pasos: 

<ol>
	<li> Abre una conexión TCP: La conexión TCP se usará para hacer una petición, o varias, y recibir la respuesta. El cliente puede abrir una conexión nueva, reusar una existente, o abrir varias a la vez hacia el servidor.</li>	
	
<li>Hacer una petición HTTP: Los mensajes HTTP (previos a HTTP/2) son legibles en texto plano. A partir de la 		versión del protocolo HTTP/2, los mensajes se encapsulan en franjas, haciendo que no sean directamente interpretables, aunque el principio de operación es el mismo.
		<pre>
		<code>
		GET / HTTP/1.1
		Host: prueba.eafit.edu.co
		Accept-Language: en
		</code>
		</pre>
	</li>
	
<li>Leer la respuesta enviada por el servidor:
		<pre>
		<code>
		HTTP/1.1 200 OK
		Date: Sat, 09 Oct 2010 14:28:02 GMT
		Server: Apache
		Last-Modified: Tue, 01 Dec 2009 20:18:22 GMT
		ETag: "51142bc1-7449-479b075b2891b"
		Accept-Ranges: bytes
		Content-Length: 29769
		Content-Type: text/html
		</code>
		</pre>
</li>
<li> Cierre o reuso de la conexión para futuras peticiones </li>
</ol>

<a name="msg"></a>
### Mensajes HTTP

Existen dos tipos de mensajes HTTP: Peticiones y respuestas, cada uno sigue su propio formato 

<a name="req"></a>
#### Peticiones

<img src="HTTP_Request.png" width="50%">

Una petición de HTTP, está formado  por los siguientes campos:

<ul>
<li>Un método HTTP,  normalmente pueden ser un verbo, como: GET, POST o un nombre como: OPTIONS o HEAD, que defina la operación que el cliente quiera realizar. El objetivo de un cliente, suele ser una petición de recursos, usando GET, o presentar un valor de un formulario HTML, usando POST, aunque en otras ocasiones puede hacer otros tipos de peticiones. </li>	
<li>La dirección del recurso pedido, la URL del recurso, sin los elementos obvios por el contexto, como pueden ser: sin el  protocolo (http://),  el dominio, o el puerto TCP. </li>
<li> La versión del protocolo HTTP.</li>
<li> Cabeceras HTTP opcionales, que pueden aportar información adicional a los servidores.
O un cuerpo de mensaje, en algún método, como puede ser POST, en el cual envía la información para el servidor. </li>
</ul>
	
<a name="res"></a>
### Respuestas

<img src="HTTP_Response.png" width="50%">

Las respuestas están formadas por los siguentes campos:

<ul>
<li>La versión del protocolo HTTP que están usando.</li>
<li>Un código de estado, indicando si la petición ha sido exitosa, o no, y debido a que. </li>
<li>Un mensaje de estado, una breve descripción del código de estado. </li>
<li>Cabeceras HTTP, como las de las peticiones. </li>
<li>Opcionalmente, el recurso que se ha pedido. </li>

</ul>

<a name="obj"></a>
## Objetivo
* Definición inicial de una arquitectura detallada para la creación de un servidor Http
* Utilizar y mejorar las habilidades de análisis detallado de arquitectura de software

<a name="desc"></a>
## Descripción general
* Para la creación de un servidor Http se debe iniciar un proceso que reciba peticiones a traves de TCP, estas peticiones llevan un mensaje con un formato especifico que debe ser interpretado de acuerdo a los estandares Http. De acuerdo al contenido del mensaje se deben ejecutar acciones específicas que hacen parte de la definición Http, sin embargo, no todas las acciones estan contempladas dentro del alcance de este proyecto.

<a name="arc"></a>
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

<a name="tool"></a>
## Herramientas de desarrollo
* El lenguaje a utilizar en el proceso de construcción del servidor Http será C#. 
* La plataforma .Net y en especial el lenguaje de programación C# es bien conocido por dos de los tres miembros del equipo y ellos servirán de soporte para mejorar el proceso de aprendizaje del ultimo integrante. 
* La herramienta seleccionada permite implementar cada una de las funcionalidades necesarias para la creación del servidor Http
* Se cuenta con suficiente documentación para crear el servidor Http con tecnología .Net.

<a name="use"></a>
## Instrucciones de uso
* Descargar el proyecto y abrir la solución en Visual Studio 2017.
* En el archivo Program.cs, se inicia el servidor en el puerto 8010. Puede cambiar esta configuración de ser necesario.
<img src="Program.cs.png" width="50%">

* Iniciar la ejecución del programa con F5 ó en el menú Debug -> Start.
* Al iniciar el programa se muestra una ventana de consola donde informa que el servidor ha sido iniciado, el puerto y la ruta del archivo donde se está registrando el log.
<img src="Consola.png" width="50%">

* Enviar una petición Http mediante un navegador o una aplicación como Postman a la ruta http://localhost:8010. Recuerde modificar el puerto 8010 en caso de ser necesario
* Revisar el archivo de Log que se encuentra en la ruta especificada por la ventana de consola.
<img src="Consola2.png" width="50%">

<a name="test"></a>
## Instrucciones para la ejecución de las pruebas de integración

<a name="vs"></a>
### Visual Studio

* En Visual Studio 2017, abrir la ventana "Test Explorer" por el menú Test -> Windows -> Test Explorer
* Dar clic en la opción "Ejecutar todos"
* Las pruebas de integración envian diferentes peticiones al Servidor http, el cual responde a todas con un código de estado 200.
<img src="IntegrationTest.cs.png" width="50%">

<a name="pm"></a>
### Postman

* Después de [ejecutar el servidor](#use), en la herramienta Postman, se da clic en el botón importar
<img src="importPM.png" width="50%">

* Se carga el archivo de "Test HttpServer.postman_collection.json" que se encuentra en la carpeta test.
<img src="archivoTestPM.png" width="50%">

* La colección aparece al lado derecho y se llama "Test HttpServer", se da clic en el botón "play" y después en el botón "Run" para abrir la ventana de ejecución.

<img src="ejecutarPruebasPM.png" width="50%">

* Se ingresa el número de ejecuciones que se desea y clic en el bóton Run Test HttpServer

<img src="ejecucionPruebasPM.png" width="50%">

### Screencast
<a name="screen"></a>
La ruta para el screencast https://www.useloom.com/share/966626f6de1a4942a9ff2099563df369


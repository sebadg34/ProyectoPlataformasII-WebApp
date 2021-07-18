# ProyectoPlataformasII-WebApp



## Tabla de contenidos 
* [Introducción](#introducción)
* [Sobre el proyecto](#proyecto)
* [Tecnologías](#tecnologías)
* [Setup](#setup)
* [Convenciones](#convenciones)

## Introducción

### Quienes Somos
Somos el equipo MakeMeister, compuesto por los siguientes integrantes

- Tomas Reimann (Director general y full Stack).
- Javiera Cordero (Base de datos y full Stack).
- Juan Barnett (Tester y full Stack).
- Sebastian Delgado (FrontEnd supervisor y full Stack).

Landing web page: https://makemeister.wixsite.com/website

### Contexto
El equipo está formado por estudiantes de la Universidad Católica del Norte, Antofagasta Chile. Con el fin de lograr el trabajo requerido para la asignatura de *Proyecto desarrollo de software basado en plataformas*.

## Proyecto
Este proyecto consta en la creación de una aplicación para realizar reservas de vuelos. El repositorio actual corresponde a la aplicación por lado del cliente. El proyecto consta de 2 partes.

- La primera corresponde a la aplicación que incluye el frontend y backend (WebApp).
- La segunda corresponde a la API que se encargará de manegar los datos y Requests (WebApi).

Este repositorio actualmente almacena la aplicación web. 
Para la webApi, dirigirse al siguiente repositorio: https://github.com/sebadg34/ProyectoPlataformas_II_WebAPI

## Tecnologías
### WebApp
* Visual Studio 2017/2019
* ASP.NET framework 15.9
* ASP.NET MVC 5.2.4
* Bootstrap 4.6.0
* Administrador paquetes NuGet 4.6.0
* JQuery 3.6.0
* Modernizr 2.8.3
* Microsoft.AspNet.WebApi.Client 5.2.7
* IText 7 7.1.16
### WebApi
* Microsoft SQL Management Studio 18
* SQL Express 2019
* Entity Framework 6.1.3
* ASP.NET WebAPI 5.2.7
* BCrypt.NET-Next 4.0.2
* ASP.NET Cors 5.2.7
* xunit 2.4.1
* xunit.runner.visualstudio 2.4.3
	
## Setup 
Para el desarrollo y ejecución del programa se deben tener los siguiente:
### Programas instalados
* Microsoft SQL Management Studio 18
* SQL Express 2019
* Visual Studio 2017/2019 instalado
* .Net Framework 4.7.*
* SO Windows 7/8/10
* Terminal git (GitBash, etc.)

### Instalación
* Por parte de la WebApp: 
> Se debe crear un repositorio local el cual almacenará la información adyaciente en el repositorio actual. Luego, se debe de instalar los paquetes y programas mencionados en la sección de [WebApp](#webapp).

* Por parte de la WebApi, ocurre de manera análoga a la instalación de la WebApp:
> Se debe de instalar los paquetes y programas mencionados en la sección de [WebApi](#webapi). Además, se debe de leer la carpeta Docs, cuya contiene la Documentación del proyecto para poder tener mejor comprensión de este. A su vez, se debe de crear una base de datos llamada "BD" dentro del programa *Microsoft SQL Management Studio 18*., dicha base de datos contendrá las entidades y atributos mencionadas dentro de la Documentación. Una vez creada, dentro del proyecto en el *Visual Studio 2017/2019* se debe ir a la sección *Web.config*, línea 68, cambiando el data source con el nombre del servidor que se muestra en *Microsoft SQL Management Studio 18*.


#### Paso a paso  de instalación

1. Abrir el terminal git.
2. Crear el directorio al cual se clonará el proyecto.
3. Iniciar el repositorio mediante:
```
git init
```
4. Crear conexión con el repositorio:
```
git remote add origin [link de la WebApp (https://github.com/sebadg34/ProyectoPlataformasII-WebApp) o la WebApi (https://github.com/sebadg34/ProyectoPlataformas_II_WebAPI)]
```
5. Finalmente, hacer pull al master:
```
git pull origin master
```

## Convenciones
### Commits
Los commits deben incluir un mensaje descriptivo de los cambios realizados
La estructura de los mensajes es la siguiente:
```
- <type>(<scope>):<subject>
```
#### type: el tipo de cambio, este pueden ser
- feat	  : adición nueva 	
- fix 	  : bug fixes (arreglo de errores)
- docs	  : cambios en la documentación
- Style	  : cambio de estilo que no afectan en la funcionalidad (formato, espaciados, etc).
- Refactor: cambio que no arregla ni agrega una funcionalidad.
- Test	  : agregar pruebas faltantes o bien corregir existentes
- Chore	  : cambios en librerías, build y herramientas auxiliares.
- perf	  : cambio que mejora el rendimiento del programa.

#### scope: Opcional, específica el lugar en donde se realiza el cambio en el commit (clase, módulo, etc).
#### subject: Descripción corta de que trata el cambio emitido.

### Programación
* Clases/Modelos: PascalCase
* Métodos: PascalCase
* Variables: camelCase
* Parámetros para Métodos: camelCase
* Json: spinal-case

### Documentación 

#### Para C#
```
///<summary>
///</summary>
* Hay que documentar las clases y métodos que se utilizan.
* Comentarios útiles sobre algo en específico dentro del código.
```

### Comentarios
Para cualquier duda o consulta sobre el proyecto, no dude en contactarse con el Director general, Tomás Reimann, mediante el siguiente correo electrónico: tomas.reimann@alumnos.ucn.cl 

Se despide atentamente, el equipo MakeMeister.


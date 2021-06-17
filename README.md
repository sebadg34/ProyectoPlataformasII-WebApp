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
### WebApi
* Microsoft SQL Management Studio 18
* SQL Express 2019
* Entity Framework 6.1.3
* ASP.NET WebAPI 5.2.7
* BCrypt.NET-Next 4.0.2
* ASP.NET Cors 5.2.7
	
## Setup 
Para el desarrollo y ejecución del programa se deben tener los siguiente:
### Programas instalados
* Microsoft SQL Management Studio 18
* SQL Express 2019
* Visual Studio 2017/2019 instalado
* .Net Framework 4.7.*
* SO Windows 7/8/10
* Terminal git (GitBash, etc.)
### Requerimientos mínimos de Hardware
* *POR DEFINIR*
### Instalación
* *POR DEFINIR*

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

### Versión
* *POR DEFINIR*

### Comentarios



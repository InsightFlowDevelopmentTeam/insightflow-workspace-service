# insightlow-workspace-service
## Proyecto que sirve como servicio para una arquitectura de microservicios
## Para levantar el proyecto se deben seguir lo siguientes pasos:

## Requisitos previos:
- .NET 9.0.304
- Visual Studio Code 1.95.3 o superior

## Instalación
1.- Primero debemos abrir la consola de comandos apretando las siguientes teclas y escribir 'cmd':

- "Windows + R" y escribimos 'cmd'

2.- Ahora debemos crear una carpeta en donde guardar el proyecto, esta carpeta puede estar donde desee el usuario:
```bash
mkdir [NombreDeCarpeta]
```
3.- Accedemoss a la carpeta.
```bash
cd NombreDeCarpeta
```
4.- Se debe clonar el repositorio en el lugar deseado por el usuario con el siguiente comando:
```bash
git clone https://github.com/InsightFlowDevelopmentTeam/insightflow-workspace-service.git
```
5.- Accedemos a la carpeta creada por el repositorio:
```bash
cd insightlow-workspace-service
```
6.- Ahora debemos restaurar las dependencias del proyecto con el siguiente comando:
```bash
dotnet restore
```
7.- Con las dependencias restauradas, abrimos el editor:
```bash
code .
```
8.- Establecer las credenciales del archivo .env
```bash
notepad .env
```
9.- Finalmente ya en el editor ejecutamos el siguiente comando para ejecutar el proyecto:
```bash
dotnet run
```

## Estructura del repositorio
- Funciona con una API de tipo REST
- Se ofrece una colección en la carpeta de Workspace de Postman en el repositorio
- Se ofrece un .env de con datos de ejemplo
- Se utiliza el Framework .NET de C#
- Utiliza endpoints para realizar el CRUD 
- Se utiliza la ruta "http://localhost:5049" para realizar las peticiones HTTP
# mexicoDestinosTest
Api NetCore con Entity Framework Base de datos Local

1.- Clonar el repositorio
2.- Abir solución
3.- Correr en la consola de administrador de paquetes el comando "Add-Migration initial-1"
4.- Correr en la consola de administrador de paquetes el comando "Update-database"
5.- Descargar y abrir postman
6.- Importar collecttion.json del proyecto en "raiz/postman"
7.- Importar environment.json del proyecto en "raiz/postman"
8.- Ejecutar proyecto con el IISExpress

Vamos a ejecutar llamadas en el orden que estan anmerdas las carpetas (postman) con el api corriendo

8.- [POST] Insertar un país
9.- [POST] Insertar un aeropuerto
10.- [POST] Insertar un destino, importante poner el id del pais creado en el paso 8
11.- [POST] Insertar una zona, importante poner el id del aeropuerto creado en el paso 9
12.- [POST] Insertar un hotel, importante poner el id del destino creado en el paso 10 y el codigo de la zona creado en el paso 11
13.- [POST] Insertar textos de cada traslado pensado, aqui se agregan por idioma uno distinto segun la cantidad de idiomas se deesee
14.- [POST] Insertar un traslado asociado al texto cargado en el paso 13 mediante el id, aqui podremos definir cuando deseamos que se gane por reservación de traslado, es decir, netRate = (nuestra tarifa con el proveedor), tax = (impuestos como el IVA en dceimales "0.16"), markUp (porcentaje de ganancia en decimales "0.12")

una vez poblado la base de datos solo nos queda utilizar los 2 request que nos ayudarán en nuestro sitio web
AUTOCOMPLETE REQ
[GET] {{host}}/api/hotels/autocomplete?term=oas

AUTOCOMPLETE RESP
[
    {
        "hotelName": "Oasis Smart",
        "zoneCode": "ZCCUN"
    }
]

SHUTTLES LIST REQ
[POST] {{host}}/api/Shuttles/Results
{
    "hotelName": "Oasis Smart",
	"zoneCode": "ZCCUN",
	"language": "ESP"
}

SHUTTLES LIST RESP
[
    {
        "id": 1,
        "airportCode": "CUN",
        "airportName": "Aeropuerto internacional de cancún",
        "zoneCode": "ZCCUN",
        "zoneName": "Centro de cancún",
        "shuttleCode": "R",
        "shuttleName": "Traslado Redondo",
        "shuttleContent": "Buen traslado muy comodo",
        "grossRate": "3,360.00",
        "totalRate": "3,897.60"
    }
]

Estos parametros ya los podemos mapear server side o client site desde una aplicación web de forma local





# Prueba Técnica - Sistema de Votaciones
## Author
- JOHAN ESTEBAN ARIAS ARBOLEDA

API RESTful para registrar votantes, candidatos y emitir votos. Implementada en .NET 8 con autenticación JWT y base de datos PostgreSQL.

---

## 🚀 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio 2022+ o VS Code](https://visualstudio.microsoft.com/es/)
-  `curl`

---

## ⚙️ Configuración

1. Clonar el repositorio:

```bash
git clone https://github.com/tu-usuario/prueba-tecnica-sistema-votaciones.git
cd prueba-tecnica-sistema-votaciones

```
2. Ir a postgreSQL, crear una base de datos y oprimir la opcion de restaurar.
- [Imagen de Ejemplo](https://github.com/ArsJohan/Prueba-Tecnica-Sistema-de-Votaciones/blob/main/docs/postgres.png)
- [Archivo Backup](https://github.com/ArsJohan/Prueba-Tecnica-Sistema-de-Votaciones/blob/main/docs/ElectoralDB.png)



3. Configurar conexión a PostgreSQL en appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=nombredetubasededatos;Username=postgres;Password=tu_password"
}

```
4. Documentación en Swagger
```
Ejecuta el proyecto desde la terminal con
dotnet run

o preciona F5

Se te abrira la url:
http://localhost:5076/swagger

Donde encontrarás el api documentada

```

5.  Poner Bearer token en swagger
- ![Paso 1](https://github.com/ArsJohan/Prueba-Tecnica-Sistema-de-Votaciones/blob/main/docs/Captura%20de%20pantalla%202025-08-05%20151548.png)
- ![Paso 2](https://github.com/ArsJohan/Prueba-Tecnica-Sistema-de-Votaciones/blob/main/docs/Captura%20de%20pantalla%202025-08-05%20151552.png)
- !Importante colocar Bearer + token Ejem. Bearer jdsjdkfjlsjdjhahsajSKJSSj.
  
## 📌 Endpoints
### Votantes
```POST /voters```
```
curl -X POST http://localhost:5076/voters \
-H "Content-Type: application/json" \
-H "Authorization: Bearer {token}" \
-d '{
  "name": "Juan Pérez",
  "email": "juan@example.com"
}'
```


```GET /voters```
```
curl -X GET http://localhost:5076/voters \
-H "Authorization: Bearer {token}"
```

```GET /voters/{id}```
```
curl -X GET http://localhost:5076/voters/1 \
-H "Authorization: Bearer {token}"
```

```DELETE /voters/{id}```
```
curl -X DELETE http://localhost:5076/voters/1 \
-H "Authorization: Bearer {token}"
```

### Candidatos
```POST /candidates```
```
curl -X POST http://localhost:5076/candidates \
-H "Content-Type: application/json" \
-H "Authorization: Bearer {token}" \
-d '{
  "name": "Ana Gómez",
  "party": "Partido Verde"
}'
```

```GET /candidates```
```
curl -X GET http://localhost:5076/candidates \
-H "Authorization: Bearer {token}"
```

```GET /candidates/{id}```
```
curl -X GET http://localhost:5076/candidates/1 \
-H "Authorization: Bearer {token}"
```

```DELETE /candidates/{id}```
```
curl -X DELETE http://localhost:5076/candidates/1 \
-H "Authorization: Bearer {token}"
```

### Votes
```POST /votes```
```
curl -X POST http://localhost:5076/votes \
-H "Content-Type: application/json" \
-H "Authorization: Bearer {token}" \
-d '{
  "voterId": 1,
  "candidateId": 2
}'
```

```GET /votes```
```
curl -X GET http://localhost:5076/votes \
-H "Authorization: Bearer {token}"
```

```GET /votes/statistics```
```
curl -X GET http://localhost:5076/votes/statistics \
-H "Authorization: Bearer {token}"
```

### Auth
```POST /Login```
```
curl -X 'POST' \
  'http://localhost:5076/login' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "username": "admin",
  "password": "1234"
}'
```


# 📊 Captura de estadísticas
![Estadísticas de votos](https://github.com/ArsJohan/Prueba-Tecnica-Sistema-de-Votaciones/blob/main/docs/Captura%20de%20pantalla%202025-08-05%20152302.png)




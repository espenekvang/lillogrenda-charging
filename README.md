# Lillogrenda.Charging
A simple application that computes invoices for each charger in Lillogrenda.

### Required user secrets
Make sure to have the following secrets set locally before running the application:
- zaptec-username
- zaptec-password

Add the secrets by running the following command for the API-project directory:
```shell
dotnet user-secrets set "{name-of-the-secret}" "{the-secret}"
```
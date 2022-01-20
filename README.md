Secret Key and Client Id are under appsettings.json File.

Handler/APIKeyHandler.cs has the logic to validate the secret key and client id passed from the http request.

This Hanlder is injected in middleware.

Player Data is loaded up in initial load, and stay in memory. 

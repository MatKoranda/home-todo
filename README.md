# MatKoranda-ToDo
Basic structure:
  -controllers
    -Authorization - manages the register and login
    -ToDo - manages the ToDos
  -models:
    -User - user entity structure
    -ToDoTask - task entity structure 
  -services
    -TokenService - manages Tokens
    -UserService - manages Users
    -ToDoService - manages ToDoTaks
 #How to deploy
 1) Create account on Azure
 2) Create new database 
 3) Import ConnectionString from your new AzureDB into 'appsettings.json' (fill in admin-username and password!)
 4) update DB
 5) update your repo
 6) Create new web app in Azure
    -choose your GitHub repository
    -let the app upload
    -to include CD, check the continuous deployment (you have to fix YML file thats automatically created in your workflow folder)
with this, every merge into Master will call this action and automatically update the deployed app on Azure!

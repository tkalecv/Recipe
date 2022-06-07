#Just right click on the script and select "Run with powershell"

#Provide SQLServerName
$SQLServer ="(localdb)\MSSQLLocalDB"

#Provide Database Name 
$DatabaseName ="RecipeDatabaseFirebase"

#Provide database initialization script
$DatabaseInitializationScriptPath ="D:\Projects\Recipe\Recipe\Recipe.DAL\DatabaseInitialization\RecipeDatabase_Initialize_Firebase.sql"

#Scripts Folder Path
$StoredProceduresFolderPath ="D:\Projects\Recipe\Recipe\Recipe.DAL\DatabaseInitialization\StoredProcedures\"

function Load-Module ($m) {

    # If module is imported say that and do nothing
    if (Get-Module | Where-Object {$_.Name -eq $m}) {
        write-host "Module $m is already imported."
    }
    else {

        # If module is not imported, but available on disk then import
        if (Get-Module -ListAvailable | Where-Object {$_.Name -eq $m}) {
            Import-Module $m -Verbose
            write-host "Module $m loaded successfully"
        }
        else {

            # If module is not imported, not available on disk, but is in online gallery then install and import
            if (Find-Module -Name $m | Where-Object {$_.Name -eq $m}) {
                Install-Module -Name $m -Force -Verbose -Scope CurrentUser
                Import-Module $m -Verbose
                write-host "Module $m installed and loaded successfully"
            }
            else {

                # If the module is not imported, not available and not in the online gallery then abort
                write-host "Module $m not imported, not available and not in an online gallery, exiting."
                EXIT 1
            }
        }
    }
}

#LoadModules
Load-Module("SqlServer")

#Enable script execution
Set-ExecutionPolicy Unrestricted
write-host "Script execution enabled"

#Create database
invoke-sqlcmd -Query "IF DB_ID('RecipeDatabaseFirebase') IS NOT NULL
                BEGIN
                    PRINT 'db exists'
                END
                ELSE
                BEGIN
                    CREATE DATABASE RecipeDatabaseFirebase;

                    PRINT 'RecipeDatabaseFirebase db created'
                END" –ServerInstance $SQLServer -Database "master"
write-host "Database created successfully"

#Run database initialization script
invoke-sqlcmd –ServerInstance $SQLServer -Database "master" -InputFile $DatabaseInitializationScriptPath
write-host "successfully executed $DatabaseInitializationScriptPath"

#Loop through the .sql files and run them
foreach ($filename in get-childitem -path $StoredProceduresFolderPath -recurse -file -filter "*.sql" | sort-object)
	{
		invoke-sqlcmd –ServerInstance $SQLServer -Database $DatabaseName -InputFile $filename.fullname

		#Print file name which is executed
		write-host "successfully executed $filename"
	}

#Disable script execution
Set-ExecutionPolicy Restricted
write-host "Script execution disabled"

Read-Host -Prompt "Press Enter to exit"
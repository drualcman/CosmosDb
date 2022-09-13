using Microsoft.Azure.Cosmos;

public class Program
{
    private static readonly string EndpointUri = "https://drualcmancosmosdb-eastus.documents.azure.com:443/";
    private static readonly string PrimaryKey = "BIxkgdwBXYEjWHmfoBlH0WG0Ae0BUablWJmJr7EpVyeBc0hNnqv8jRnJ6r3JhQaZNMVJuCJK6KGEsotlSYUM8A==";

    private CosmosClient cosmosClient;
    private Database database;
    private Container container;

    private string DatabaseId = "cosmosDatabase";
    private string ContainerId = "cosmosContaniner";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Iniciando operaciones...\n");
            Program p = new Program();
            // Oara ejecutar las operaciones con Cosmos.
            await p.CosmosAsync();
        }
        catch(CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} Ocurrio un error: {1}", de.StatusCode, de);
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine(
                "Fin del programa, presiona cualquier tecla para finalizar.");
            Console.ReadKey();
        }
    }

    public async Task CosmosAsync()
    {
        // Crear una instancia del cliente Cosmos
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // Ejecutar el método que crea la base de datos
        await this.CreateDatabaseAsync();

        // Ejecutar el método que crea el contenedor
        await this.CreateContainerAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        Console.WriteLine("Base de datos creada {0}\n", database.Id);
    }

    private async Task CreateContainerAsync()
    {
        container = await database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        Console.WriteLine("Contenedor creado {0}\n", container.Id);
    }
}
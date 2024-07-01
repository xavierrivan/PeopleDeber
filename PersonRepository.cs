using SQLite;
using PeopleDeber.Models;

namespace PeopleDeber;
public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteConnection conn;

    private void Init()
    {
        // TODO: Add code to initialize the repository         
        if (conn != null)
            return;
        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();
            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            Person person = new Person
            {
                Name = name
            };
            result = conn.Insert(person);

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }
    public void AddNewPerson(Person persona)
    {
        try
        {
            conn.Insert(persona);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", ex.Message);
            throw;
        }
    }

    public void UpdatePerson(Person persona)
    {
        try
        {
            Init();
            conn.Update(persona);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public List<Person> GetAllPeople()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init();
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }


    public Person GetPerson(int id)
    {
        try
        {
            Init();
            var people = conn.Table<Person>().ToList();
            Person person = people.Find(x => x.Id == id);
            return person;
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new Person();
    }
    public void DeletePerson(Person person)
    {
        try
        {
            Init();
            conn.Delete(person);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
    }
}
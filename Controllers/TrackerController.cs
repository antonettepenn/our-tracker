using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OurTracker.Models;
using System.Collections.Generic;

public class TrackerController : Controller
{
    private readonly string _connectionString = "Data Source=ourtracker.db";

    public IActionResult Index()
    {
        var entries = new List<Entry>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Entries ORDER BY CreatedAt DESC";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            entries.Add(new Entry
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                Category = reader.GetString(3),
                IsCompleted = reader.GetBoolean(4),
                CreatedAt = reader.GetDateTime(5)
            });
        }

        return View(entries);
    }
    [HttpGet]
    public IActionResult Add() => View();

    // add new entry to DB
    [HttpPost]
    public IActionResult Add(Entry entry)
    {
        entry.CreatedAt = DateTime.Now;

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Entries (Title, Description, Category, IsCompleted, CreatedAt)
            VALUES ($Title, $Description, $Category, $IsCompleted, $CreatedAt);
        ";
        
        command.Parameters.AddWithValue("$Title", entry.Title);
        command.Parameters.AddWithValue("$Description", entry.Description);
        command.Parameters.AddWithValue("$Category", entry.Category);
        command.Parameters.AddWithValue("$IsCompleted", entry.IsCompleted);
        command.Parameters.AddWithValue("$CreatedAt", entry.CreatedAt);
        
        command.ExecuteNonQuery();
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ToggleComplete(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var getStatus = connection.CreateCommand();
        getStatus.CommandText = "SELECT IsCompleted FROM Entries WHERE Id = $id";
        getStatus.Parameters.AddWithValue("$id", id);
        var isCompleted = (bool) getStatus.ExecuteScalar();

        var toggle = connection.CreateCommand();
        toggle.CommandText = "UPDATE Entries SET IsCompleted = $newStatus WHERE Id = $id";
        toggle.Parameters.AddWithValue("$newStatus", isCompleted);
        toggle.Parameters.AddWithValue("$id", id);
        
        toggle.ExecuteNonQuery();
        
        return RedirectToAction("Index");
    }
    
}
    

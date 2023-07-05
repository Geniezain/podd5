using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoggingMicroservice.Data;
using LoggingMicroservice.Models;

namespace LoggingMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<LogsController> _logger;

        public LogsController(DataContext context, ILogger<LogsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Log>> Get() 
        {
             if (_context.Logs == null)
            {
                return NotFound();
            }

            var logs = _context.Logs.AsQueryable();
            return Ok(logs);
        }

        [HttpPost]
        public IActionResult PostLog(Log log)
        {
             if (_context.Logs == null)
            {
                return NotFound();
            }

            
            log.CreatedAt = DateTime.Now;
            _context.Logs.Add(log);
            _context.SaveChanges();
            _logger.LogInformation("New log: {@Log}", log);
            

    
            string logEntry = $"Log Entry: {DateTime.Now}\n" +
                      $"Application: {log.Application}\n" +
                      $"LogLevel: {log.LogLevel}\n" +
                      $"Message: {log.Message}\n" +
                      $"Serverity: {log.Message}\n"+
                      $"HostName: {log.HostName}\n"+
                      $"Associated Id: {log.AssociateId}\n"+
                      $"Technology: {log.Technology}\n"+
                      $"ModuleName: {log.ModuleName}\n"+
                      $"FeatureName: {log.FeatureName}\n"+
                      $"ClassName: {log.ClasName}\n"+
                      $"ErrorCode: {log.ErrorCode}\n"+
                      $"Error Message: {log.ErrMessage}\n"+
                      $"----------------------------------\n";

    
            string directoryPath = "logs"; 
            string filePath = Path.Combine(directoryPath, $"logs_{DateTime.Now:yyyy-MM-dd}.txt");

             
    Directory.CreateDirectory(directoryPath);
    System.IO.File.AppendAllText(filePath, logEntry);


    return CreatedAtAction(nameof(GetLog), new { id = log.Id }, log);
}

        [HttpGet("{id}")]
        public IActionResult GetLog(int id)
        {
             if (_context.Logs == null)
            {
                return NotFound();
            }

            var log = _context.Logs.Find(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        
    }
}

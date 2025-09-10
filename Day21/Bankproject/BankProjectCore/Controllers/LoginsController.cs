﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankProjectCore.Models;
using BankProjectCore.Middleware;

namespace BankProjectCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly BankDbContext _context;

        public LoginsController(BankDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("/login/{username}/{password}")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            Login loginFound = await _context.Logins.Where(x => x.UserName == username).FirstOrDefaultAsync();

            if (loginFound == null)
            {
                return "0";
            }
            string? encry = loginFound.Password;
            string pwd = DecryptionHelper.Decrypt(encry);
            if (pwd.Equals(password))
            {
                return "1";
            }
            return "0";
        }

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostLogin(Login login)
        {
            string pwd = EncryptionHelper.Encrypt(login.Password);
            login.Password = pwd;
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            return "Account Created Successfully...";
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginExists(int id)
        {
            return _context.Logins.Any(e => e.Id == id);
        }
    }
}
using Data.Processing.Context;
using Data.Processing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Processing.Services
{
    public class BloopService : IBloopService
    {
        private readonly BloopDbContext _db;

        public BloopService(BloopDbContext db)
        {
            _db = db;
        }

        public async Task<BloopResponse<Sensitiveword>> CreateAsync(Sensitiveword word)
        {
            try
            {
                _db.Sensitivewords.Add(word);
                await _db.SaveChangesAsync();
                return new BloopResponse<Sensitiveword>(true, "Successfully Added", word);
            }
            catch(Exception ex)
            {
                return new BloopResponse<Sensitiveword>(false, ex.Message, word);
            }
        }
        public async Task<BloopResponse<Sensitiveword>> DeleteAsync(int id)
        {
            Sensitiveword word = null;
            try
            {
                word = await _db.Sensitivewords.FindAsync(id);
                if(word == null)
                {
                    return new BloopResponse<Sensitiveword>(false, "Word not found", null);
                }

                _db.Sensitivewords.Remove(word);
                await _db.SaveChangesAsync();
                return new BloopResponse<Sensitiveword>(true, "Successfully Deleted", word);
            }
            catch(Exception ex)
            {
                return new BloopResponse<Sensitiveword>(false, ex.Message, word);
            }
        }
        public async Task<BloopResponse<IEnumerable<Sensitiveword>>> GetAsync()
        {
            try
            {
                return new BloopResponse<IEnumerable<Sensitiveword>>(true, "", await _db.Sensitivewords.ToListAsync());
            }
            catch(Exception ex)
            {
                return new BloopResponse<IEnumerable<Sensitiveword>>(false, ex.Message, null);
            }
        }
        public async Task<BloopResponse<Sensitiveword>> GetAsync(int id)
        {
            try
            {
                return new BloopResponse<Sensitiveword>(true, "", await _db.Sensitivewords.FindAsync(id));
            }
            catch(Exception ex)
            {
                return new BloopResponse<Sensitiveword>(false, ex.Message, null);
            }
        }

        public async Task<BloopResponse<IEnumerable<Sensitiveword>>> ImportAsync(List<Sensitiveword> words)
        {            
            try
            {
                await _db.Sensitivewords.AddRangeAsync(words);
                await _db.SaveChangesAsync();
                return new BloopResponse<IEnumerable<Sensitiveword>> (true, "Successfully Imported", words);
            }
            catch (Exception ex)
            {
                return new BloopResponse<IEnumerable<Sensitiveword>> (false, ex.Message, words);
            }
        }

        public async Task<BloopResponse<Sensitiveword>> UpdateAsync(Sensitiveword word)
        {
            try
            {
                word.ModifiedOn = DateTime.Now;
                _db.Entry(word).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return new BloopResponse<Sensitiveword>(true, "Successfully Updated", word);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return new BloopResponse<Sensitiveword>(false, ex.Message, null);
            }
        }
    }
}

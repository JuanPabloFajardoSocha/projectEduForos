using eduforos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Infrastructure.Persistence
{
    internal class SubjectRepository : ISubjectRepository
    {
        private readonly EduForosDbContext _context;

        public SubjectRepository(EduForosDbContext context) { _context = context; }

        public bool DeleteSubject(int idSubject)
        {
            bool status=false;
            var response=_context.Subjects.FirstOrDefault(s => s.IdSubject == idSubject);

            if (response != null)
            {
                try
                {
                    _context.Subjects.Remove(response);
                    status = true;
                }catch(Exception ex) 
                {
                    Console.WriteLine(ex);
                }finally
                { 
                    _context.SaveChanges();
                }
               
            }
            return status;  
        }

        public List<Subject> GetAllSubjects()
        {
            List<Subject> list = _context.Subjects.ToList();
            return list;
            
        }

        public Subject GetSubjectById(int id)
        {
            Subject subject = _context.Subjects.FirstOrDefault(s => s.IdSubject == id);
            return subject;
        }

        public Subject GetSubjectByName(string name)
        {
            Subject subject = _context.Subjects.FirstOrDefault(s => s.Name == name);
            return subject;
        }      

        public bool RegisterSubject(Subject subject)
        {
            bool status=false;
            try
            {
                _context.Subjects.Add(subject);
                status=true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                _context.SaveChanges();
            }
            return status;
        }
    }
}

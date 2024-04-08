using eduforos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.SubjectService
{
    internal class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository) 
        {
            _subjectRepository = subjectRepository;
        }

        public bool DeleteSubject(int idSubject)
        {
            var response=_subjectRepository.DeleteSubject(idSubject);
            if (response)
            {
                return true;
            }
            else
            {
                throw new Exception("No se pudo eliminar la asignatura, Intentelo de Nuevo");  
            }
        }

        public List<Subject> GetSubjects()
        {
            var response=_subjectRepository.GetAllSubjects();
            if (response.Count == 0){
                throw new Exception("No hay asignaturas registradas");
            }else
            {
                return response;    
            }
        }


        public bool RegisterSubject(string name)
        {
            bool status=false;
            if (name.IsNullOrEmpty())
            {
                throw new Exception("El nombre no puede ser vacio");
            }

            var result= _subjectRepository.GetSubjectByName(name);

            if(result!=null)
            {
                throw new Exception("La asignatura ya esta registrada");
            }
            else
            {
                var response=_subjectRepository.RegisterSubject(new Subject
                {
                    Name = name,
                });
                if (response)
                {
                    status=true;
                }
            }
            return status;
            
        }
    }
}

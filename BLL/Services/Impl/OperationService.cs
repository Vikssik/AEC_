using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services.Impl
{
    public class OperationService : IOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            // Ініціалізація мапера
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Operation, OperationDTO>();
            });
            _mapper = config.CreateMapper();
        }

        // Перевірка доступу до операції для користувача
        public bool CheckOperationAccess(int userId, string operationName)
        {
            var user = _unitOfWork.Users.Get(userId);
            var userType = user.GetType();

            // Технічний спеціаліст може виконувати лише обслуговування технічного обладнання та оновлення документації
            if (userType == typeof(TechnicalSpecialist))
            {
                return operationName == "ServiceSensors" || operationName == "ServiceEquipment" || operationName == "UpdateDocumentation";
            }

            // Адміністратор має доступ до CRUD операцій для користувачів
            if (userType == typeof(SystemAdministrator))
            {
                return operationName == "CreateUser" || operationName == "ReadUser" ||
                       operationName == "UpdateUser" || operationName == "DeleteUser";
            }

            // Інспектор безпеки має доступ лише до аудиту та звіту про стан системи
            if (userType == typeof(SafetyInspector))
            {
                return operationName == "AuditSystem" || operationName == "GenerateSystemReport";
            }

            // Якщо роль не знайдена, доступ заборонений
            throw new MethodAccessException("Access denied for this operation.");
        }

        // Отримання операцій для певної ролі
        public IEnumerable<OperationDTO> GetOperationsByRole(string roleName)
        {
            IEnumerable<Operation> operationsEntities;

            if (roleName == nameof(TechnicalSpecialist))
            {
                operationsEntities = _unitOfWork.Operations.Find(o => o.Name == "ServiceSensors" || o.Name == "ServiceEquipment" || o.Name == "UpdateDocumentation").ToList();
            }
            else if (roleName == nameof(SystemAdministrator))
            {
                operationsEntities = _unitOfWork.Operations.Find(o => o.Name == "CreateUser" || o.Name == "ReadUser" ||
                                                                      o.Name == "UpdateUser" || o.Name == "DeleteUser").ToList();
            }
            else if (roleName == nameof(SafetyInspector))
            {
                operationsEntities = _unitOfWork.Operations.Find(o => o.Name == "AuditSystem" || o.Name == "GenerateSystemReport").ToList();
            }
            else
            {
                throw new Exception("Role not recognized.");
            }

            // Перетворення сутностей операцій в DTO
            return _mapper.Map<IEnumerable<Operation>, List<OperationDTO>>(operationsEntities);
        }
    }
}

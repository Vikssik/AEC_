using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services.Impl
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            // Ініціалізація мапера
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Permission, PermissionDTO>();
            });
            _mapper = config.CreateMapper();
        }

        // Перевірка доступу до дозволу для користувача
        public bool CheckPermission(int userId, string permissionName)
        {
            var user = _unitOfWork.Users.Get(userId);
            var userType = user.GetType();

            // Технічний спеціаліст має доступ лише до дозволів на обслуговування технічного обладнання та оновлення документації
            if (userType == typeof(TechnicalSpecialist))
            {
                return permissionName == "ServiceSensors" || permissionName == "ServiceEquipment" || permissionName == "UpdateDocumentation";
            }

            // Адміністратор має доступ до CRUD дозволів для користувачів
            if (userType == typeof(SystemAdministrator))
            {
                return permissionName == "CreateUser" || permissionName == "ReadUser" ||
                       permissionName == "UpdateUser" || permissionName == "DeleteUser";
            }

            // Інспектор з безпеки має доступ лише до дозволу на аудит та звіт про стан системи
            if (userType == typeof(SafetyInspector))
            {
                return permissionName == "AuditSystem" || permissionName == "GenerateSystemReport";
            }

            // Якщо роль не знайдена, доступ заборонений
            throw new MethodAccessException("Access denied for this permission.");
        }

        // Отримання дозволів для певної ролі
        public IEnumerable<PermissionDTO> GetPermissionsByRole(string roleName)
        {
            IEnumerable<Permission> permissionsEntities;

            if (roleName == nameof(TechnicalSpecialist))
            {
                permissionsEntities = _unitOfWork.Permissions.Find(p => p.Name == "ServiceSensors" || p.Name == "ServiceEquipment" || p.Name == "UpdateDocumentation").ToList();
            }
            else if (roleName == nameof(SystemAdministrator))
            {
                permissionsEntities = _unitOfWork.Permissions.Find(p => p.Name == "CreateUser" || p.Name == "ReadUser" ||
                                                                      p.Name == "UpdateUser" || p.Name == "DeleteUser").ToList();
            }
            else if (roleName == nameof(SafetyInspector))
            {
                permissionsEntities = _unitOfWork.Permissions.Find(p => p.Name == "AuditSystem" || p.Name == "GenerateSystemReport").ToList();
            }
            else
            {
                throw new Exception("Role not recognized.");
            }

            // Перетворення сутностей дозволів в DTO
            return _mapper.Map<IEnumerable<Permission>, List<PermissionDTO>>(permissionsEntities);
        }
    }
}

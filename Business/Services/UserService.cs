using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        public List<User> GetAllUsers(string account = null, string name = null, int? role = null, bool? isActive = null)
        {
            try
            {
                return _userRepository.GetUsers(account, name, role, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取用户列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        public User GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user == null)
                {
                    throw new Exception($"ID为 {id} 的用户不存在");
                }
                return user;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取用户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据账户名获取用户
        /// </summary>
        public User GetUserByAccount(string account)
        {
            try
            {
                if (string.IsNullOrEmpty(account))
                {
                    throw new Exception("账户名不能为空");
                }

                return _userRepository.GetUserByAccount(account);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取用户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser(User user)
        {
            try
            {
                // 验证必要字段
                ValidateUserRequiredFields(user);

                // 验证账户名是否唯一
                ValidateAccountUnique(user.Account, null);

                // 设置默认值
                if (user.CreatedAt == DateTime.MinValue)
                {
                    user.CreatedAt = DateTime.Now;
                }

                // 注意：在实际应用中应该对密码进行加密
                return _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建用户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public bool UpdateUser(User user)
        {
            try
            {
                // 验证用户是否存在
                var existingUser = _userRepository.GetUserById(user.Id);
                if (existingUser == null)
                {
                    throw new Exception($"ID为 {user.Id} 的用户不存在");
                }

                // 验证必要字段
                ValidateUserRequiredFields(user);

                // 验证账户名是否唯一（排除当前用户）
                ValidateAccountUnique(user.Account, user.Id);

                // 注意：在实际应用中应该对密码进行加密
                return _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新用户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public bool DeleteUser(int id)
        {
            try
            {
                // 验证用户是否存在
                var existingUser = _userRepository.GetUserById(id);
                if (existingUser == null)
                {
                    throw new Exception($"ID为 {id} 的用户不存在");
                }

                return _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除用户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        public User Authenticate(string account, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("账户名和密码不能为空");
                }

                var user = _userRepository.GetUserByAccount(account);
                if (user == null)
                {
                    throw new Exception("账户名或密码错误");
                }

                if (!user.IsActive)
                {
                    throw new Exception("该用户已被禁用");
                }

                // 注意：在实际应用中应该对密码进行加密验证
                if (user.Password != password)
                {
                    throw new Exception("账户名或密码错误");
                }

                return user;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"用户登录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证用户必要字段
        /// </summary>
        private void ValidateUserRequiredFields(User user)
        {
            if (string.IsNullOrEmpty(user.Account))
            {
                throw new Exception("账户名不能为空");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("密码不能为空");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new Exception("用户名不能为空");
            }
        }

        /// <summary>
        /// 验证账户名是否唯一
        /// </summary>
        private void ValidateAccountUnique(string account, int? excludeId)
        {
            var users = _userRepository.GetUsers(account: account);
            if (users.Exists(u => u.Account == account && u.Id != excludeId))
            {
                throw new Exception($"账户名 '{account}' 已存在");
            }
        }
    }
}
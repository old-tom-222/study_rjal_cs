using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class PaymentMethodService
    {
        private readonly PaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService()
        {
            _paymentMethodRepository = new PaymentMethodRepository();
        }

        /// <summary>
        /// 获取所有付款方式
        /// </summary>
        public List<PaymentMethod> GetAllPaymentMethods(string code = null, string name = null, bool? isActive = null)
        {
            try
            {
                return _paymentMethodRepository.GetPaymentMethods(code, name, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取付款方式列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取付款方式
        /// </summary>
        public PaymentMethod GetPaymentMethodById(int id)
        {
            try
            {
                var paymentMethod = _paymentMethodRepository.GetPaymentMethodById(id);
                if (paymentMethod == null)
                {
                    throw new Exception($"ID为 {id} 的付款方式不存在");
                }
                return paymentMethod;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取付款方式失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建付款方式
        /// </summary>
        public int CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                // 验证必要字段
                ValidatePaymentMethodRequiredFields(paymentMethod);

                // 验证付款方式代码是否唯一
                ValidatePaymentMethodCodeUnique(paymentMethod.Code, null);

                // 设置默认值
                if (paymentMethod.CreatedAt == DateTime.MinValue)
                {
                    paymentMethod.CreatedAt = DateTime.Now;
                }

                return _paymentMethodRepository.CreatePaymentMethod(paymentMethod);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建付款方式失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新付款方式
        /// </summary>
        public bool UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            try
            {
                // 验证付款方式是否存在
                var existingPaymentMethod = _paymentMethodRepository.GetPaymentMethodById(paymentMethod.Id);
                if (existingPaymentMethod == null)
                {
                    throw new Exception($"ID为 {paymentMethod.Id} 的付款方式不存在");
                }

                // 验证必要字段
                ValidatePaymentMethodRequiredFields(paymentMethod);

                // 验证付款方式代码是否唯一（排除当前付款方式）
                ValidatePaymentMethodCodeUnique(paymentMethod.Code, paymentMethod.Id);

                // 设置更新时间
                paymentMethod.UpdatedAt = DateTime.Now;

                return _paymentMethodRepository.UpdatePaymentMethod(paymentMethod);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新付款方式失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除付款方式
        /// </summary>
        public bool DeletePaymentMethod(int id)
        {
            try
            {
                // 验证付款方式是否存在
                var existingPaymentMethod = _paymentMethodRepository.GetPaymentMethodById(id);
                if (existingPaymentMethod == null)
                {
                    throw new Exception($"ID为 {id} 的付款方式不存在");
                }

                return _paymentMethodRepository.DeletePaymentMethod(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除付款方式失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证付款方式必要字段
        /// </summary>
        private void ValidatePaymentMethodRequiredFields(PaymentMethod paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod.Code))
            {
                throw new Exception("付款方式代码不能为空");
            }

            if (string.IsNullOrEmpty(paymentMethod.Name))
            {
                throw new Exception("付款方式名称不能为空");
            }
        }

        /// <summary>
        /// 验证付款方式代码是否唯一
        /// </summary>
        private void ValidatePaymentMethodCodeUnique(string code, int? excludeId)
        {
            var paymentMethods = _paymentMethodRepository.GetPaymentMethods(code: code);
            if (paymentMethods.Exists(p => p.Code == code && p.Id != excludeId))
            {
                throw new Exception($"付款方式代码 '{code}' 已存在");
            }
        }
    }
}
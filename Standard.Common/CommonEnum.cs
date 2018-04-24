using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Common
{
    public class CommonEnum
    {

        public enum BatchOperation
        {
            Add,
            Delete,
            Modify
        }

        /// <summary>
        /// 单据页面状态
        /// </summary>
        public enum BillState
        {
            /// <summary>
            /// 浏览
            /// </summary>
            Browse = 1,
            /// <summary>
            /// 新增
            /// </summary>
            Add = 2,
            /// <summary>
            /// 修改
            /// </summary>
            Modify = 3,
            /// <summary>
            /// 审核
            /// </summary>
            Auditing = 4,
            /// <summary>
            /// 作废
            /// </summary>
            Void = 5,
            /// <summary>
            /// 删除（暂时无用）
            /// </summary>
            Delete = 6
        }

        public enum NonKendoUIControl
        {
            CheckBox,
            RadioButton
        }


        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetEnumDescription(Enum value)
        {
            Type enumType = value.GetType();
            // 获取枚举常数名称。
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            return string.Empty;
        }

        public static string GetEnumIntString(Enum value)
        {
            return Convert.ToInt32(value).ToString();
        }

        /// <summary>
        /// 审核结果
        /// </summary>
        public enum WorkFlowAuditResult
        {
            /// <summary>
            /// 回退
            /// </summary>
            NotThrough = 0,
            /// <summary>
            /// 通过
            /// </summary>
            Through = 1,
            /// <summary>
            /// 转交
            /// </summary>
            Transfer = 2,
            /// <summary>
            /// 会签
            /// </summary>
            CounterSign = 3,
            /// <summary>
            /// 受理
            /// </summary>
            Acceptance = 4,
            /// <summary>
            /// 制文
            /// </summary>
            ProductionDoc = 5,
            /// <summary>
            /// 办结
            /// </summary>
            Approval = 6,
            /// <summary>
            /// 回复
            /// </summary>
            Reply = 7,

            /// <summary>
            /// 暂不审批
            /// </summary>
            TemporarilyNoAuditing = 8,

            /// <summary>
            /// 加签（转下一步）
            /// </summary>
            AddSign = 9,
            /// <summary>
            /// 不予受理
            /// </summary>
            NotAcceptance = 10,

            /// 退件
            /// </summary>
            RejectedReport = 11,

            /// 报件中止
            /// </summary>
            BreakDownReport = 12,

            /// 报件恢复
            /// </summary>
            ReBreakDownReport = 13
        }

        /// <summary>
        /// 生成编码类型
        /// </summary>
        public enum GenerateCodeType
        {
            /// <summary>
            /// 受理编码
            /// </summary>
            AcceptanceCode = 1,
            /// <summary>
            /// 项目编号
            /// </summary>
            ProjectCode = 2,
            /// <summary>
            /// 企业编号
            /// </summary>
            PolluteCode = 3

        }

        /// <summary>
        /// 批文状态（编辑，只读，不显示)
        /// </summary>
        public enum ApprovalStatus
        {
            Hiden,
            ReadOnly,
            Editable
        }
        /// <summary>
        /// 批文文号来源类型（报件，发函，复函)
        /// </summary>
        public enum DocNumComeFormType
        {
            [Description("报件")]
            ProjectReport = 1,
            [Description("发函")]
            SentLetter = 2,
            [Description("复函")]
            ReceivedLetter = 3
        }




        public enum ProjectReportAuditResult
        {

            /// <summary>
            /// 审批通过
            /// </summary>
            Approval = 1,
            /// <summary>
            /// 暂不审批
            /// </summary>
            TempNoAuditing = 2,
            /// <summary>
            /// 退件无函
            /// </summary>
            ReturnNoLetter = 3,
            /// <summary>
            /// 退件有函
            /// </summary>
            ReturnHaveLetter = 4,
        }

        /// <summary>
        /// 企业状态
        /// </summary>
        public enum FirmStatus
        {
            /// <summary>
            /// 待审核
            /// </summary>
            BeAccepted = 0,
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 退回
            /// </summary>
            BeReturned = -1,

            /// <summary>
            /// 已注销
            /// </summary>
            IsLogout = 3,

        }

        /// <summary>
        /// 项目补录类型
        /// </summary>
        public enum ProjectSupplyType
        {
            /// <summary>
            /// 国家项目补录
            /// </summary>
            CountryProject = 1,
            /// <summary>
            /// 历史项目补录
            /// </summary>
            HistoryProject = 2,

        }


        /// <summary>
        /// 报件状态Code
        /// </summary>
        public enum ReportProcessStausCode
        {
            /// <summary>
            /// 暂存未提交
            /// </summary>          
            [Description("暂存未提交")]
            UnCommitted = 0,
            /// <summary>
            /// 已提交待受理
            /// </summary>          
            [Description("已提交待受理")]
            BeAccepted = 1,
            /// <summary>
            /// 正在办理
            /// </summary>          
            [Description("正在办理")]
            Acceptance = 2,
            /// <summary>
            /// 不予受理
            /// </summary>          
            [Description("不予受理")]
            NotAcceptance = 3,
            /// <summary>
            /// 审批通过
            /// </summary>          
            [Description("审批通过")]
            AuditCompleted = 4,
            /// <summary>
            /// 暂不审批
            /// </summary>          
            [Description("暂不审批")]
            TempNoAuditing = 5,
            /// <summary>
            /// 退件
            /// </summary>          
            [Description("退件")]
            BeReturned = 6,
            /// <summary>
            /// 已取件
            /// </summary>          
            [Description("已取件")]
            BeDelivery = 7,
            /// <summary>
            /// 已撤回
            /// </summary>          
            [Description("已撤回")]
            BeRevert = 8,
            /// <summary>
            /// 已制文
            /// </summary>          
            [Description("已制文")]
            BeProductDoc = 9,
            /// <summary>
            /// 已办结
            /// </summary>          
            [Description("已办结")]
            BeReply = 10,
            /// <summary>
            /// 项目中止
            /// </summary>          
            [Description("项目中止")]
            BreakDown = 11
        }

        /// <summary>
        /// 消息来源编码
        /// </summary>
        public enum MessageReSourceCode
        {
            /// <summary>
            /// 报件审批
            /// </summary>
            ProjectReportApproval = 1,
            /// <summary>
            /// 函审批
            /// </summary>
            LetterApproval = 2,
            /// <summary>
            /// 抄送
            /// </summary>
            ProjectSend = 3,
            /// <summary>
            /// 移交
            /// </summary>
            ProjectTransfer = 4,
            /// <summary>
            /// 催办消息
            /// </summary>
            ProjectReportUrge = 5
        }

        /// <summary>
        /// 审批过程中批文类型
        /// </summary>
        public enum ProjectDeclareApprovalDocType
        {
            None = 0,
            Approvals = 1,
            Letters = 2
        }

        /// <summary>
        /// 接口参数Flag
        /// </summary>
        public enum OutputApiParamFlag
        {
            Add = 1,
            Update = 2,
            Delete = 3,
            Other = 4
        }
        /// <summary>
        /// 网审平台接口类型
        /// </summary>
        public enum WSPTInterfaceType
        {
            /// <summary>
            /// 获取表单结构
            /// </summary>
            hqbdjg,
            /// <summary>
            /// 获取流程结构
            /// </summary>
            hqlcjg,
            /// <summary>
            /// 提交表单
            /// </summary>
            tjbd,
            /// <summary>
            /// 修改表单数据
            /// </summary>
            xgbdsj,
            /// <summary>
            /// 项目业务受理
            /// </summary>
            xmywsl,
            /// <summary>
            /// 提交业务办理过程
            /// </summary>
            tjywblgc,
            /// <summary>
            /// 提交办理结果
            /// </summary>
            tjbljg,
            /// <summary>
            /// 业务挂起
            /// </summary>
            ywgq,
            /// <summary>
            /// 业务解除挂起
            /// </summary>
            ywjcgq,
            /// <summary>
            /// 业务回退
            /// </summary>
            ywht,
            /// <summary>
            /// 业务退件
            /// </summary>
            ywtj,
            /// <summary>
            /// 附件上传
            /// </summary>
            fjsc
        }

        /// <summary>
        /// 网审平台请求返回结果
        /// </summary>
        public enum WSPTResultCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            Succees = 200,
            /// <summary>
            /// 未知异常
            /// </summary>
            UnknowException = 300
        }

        /// <summary>
        /// 网审平台材料类型
        /// </summary>
        public enum WSPTFileType
        {
            /// <summary>
            /// 纸质件
            /// </summary>
            ZZJ = 0,
            /// <summary>
            /// 电子档
            /// </summary>
            DZD = 1,
            /// <summary>
            /// 电子证照
            /// </summary>
            DZZ = 2
        }

        /// <summary>
        /// 短信类型
        /// </summary>
        public enum SMSSendType
        {
            /// <summary>
            /// 报件提交（市级）
            /// </summary>
            bjtj,
            /// <summary>
            /// 审批通过（审批人）
            /// </summary>
            sptgspr,
            /// <summary>
            /// 审批通过（报件人）
            /// </summary>
            sptgbjr,
            /// <summary>
            /// 报件办结
            /// </summary>
            bjbj,
            /// <summary>
            /// 退件
            /// </summary>
            tj,
            /// <summary>
            /// 暂不审批
            /// </summary>
            zbsp,
            /// <summary>
            /// 会签
            /// </summary>
            hq,
            /// <summary>
            /// 催办
            /// </summary>
            cb,
            /// <summary>
            /// 抄送、移交
            /// </summary>
            csyj,
            /// <summary>
            /// 局领导代办事项
            /// </summary>
            jlddbsx,
            /// <summary>
            /// 即将到期提醒
            /// </summary>
            jjdqtx,
            /// <summary>
            /// 到期提醒
            /// </summary>
            dqtx,
            /// <summary>
            /// 区县受理
            /// </summary>  
            qxsl
        }
        /// <summary>
        /// 运营商类型
        /// </summary>
        public enum SMSCompanyType
        {
            /// <summary>
            /// 移动
            /// </summary>
            Mobile,
            /// <summary>
            /// 联通
            /// </summary>
            Unicom,
        }

        #region 大厅推送审批状态

        /// <summary>
        /// 大厅系统：报件登记-报件状态
        /// </summary>
        public class JJ_BJDJ_BJZT
        {
            /// <summary>
            /// 正在办理
            /// </summary>
            public static string Acceptance = "00";
            /// <summary>
            /// 办结
            /// </summary>
            public static string AuditOver = "01";

            /// <summary>
            /// 取件
            /// </summary>
            public static string BeDelivery = "02";
            /// <summary>
            /// 延期
            /// </summary>
            public static string Delay = "99";

            /// <summary>
            /// 项目中止
            /// </summary>
            public static string ProjectBreakDown = "100";
        }
        /// <summary>
        /// 大厅系统：报件登记-审批结果
        /// </summary>
        public class JJ_BJDJ_SPJG
        {
            /// <summary>
            /// 正在办理
            /// </summary>
            public static string Acceptance = "00";
            /// <summary>
            /// 审批通过
            /// </summary>
            public static string Audit = "01";
            /// <summary>
            /// 退件
            /// </summary>
            public static string RejectReport = "02";
        }

        /// <summary>
        /// 大厅系统：审批意见-审批状态状态
        /// </summary>
        public class JG_SPYJ_SPZT
        {
            /// <summary>
            /// 审批中
            /// </summary>
            public static string Acceptance = "01";
            /// <summary>
            /// 审批结束（未制文）
            /// </summary>
            public static string Audit = "02";
            /// <summary>
            /// 办结
            /// </summary>
            public static string AuditOver = "03";
            /// <summary>
            /// 延期
            /// </summary>
            public static string Delay = "99";

            /// <summary>
            /// 项目中止
            /// </summary>
            public static string ProjectBreakDown = "04";
        }
        /// <summary>
        /// 大厅系统：审批意见-审批结果
        /// </summary>
        public class JG_SPYJ_SPJG
        {
            /// <summary>
            /// 审批中
            /// </summary>
            public static string Acceptance = "01";
            /// <summary>
            /// 审批通过
            /// </summary>
            public static string Audit = "02";
            /// <summary>
            /// 退件
            /// </summary>
            public static string RejectReport = "03";
        }
        #endregion
    }
}

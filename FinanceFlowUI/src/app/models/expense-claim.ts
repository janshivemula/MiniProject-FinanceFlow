export enum ExpenseStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2
}

export interface IExpenseClaimResponse {
  id: number;
  employeeId: number;
  employeeName: string;
  departmentId: number;
  departmentName: string;
  expenseCategoryId: number;
  expenseCategoryName: string;
  amount: number;
  expenseDate: string;
  description: string;
  status: string;
  reviewRemark?: string;
  approvedBy?: string;
  approvedDate?: string;
}
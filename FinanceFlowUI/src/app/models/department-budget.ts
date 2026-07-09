export interface IDepartmentBudget {
  id: number;
  departmentId: number;
  departmentName?: string;
  month: number;
  year: number;
  budgetAmount: number;
  isActive: boolean;
}
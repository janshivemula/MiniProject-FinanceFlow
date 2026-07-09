export enum UserRole {
  Employee = 0,
  FinanceManager = 1
}

export interface IEmployee {
  employeeId: number;
  employeeName: string;
  email: string;
  phoneNumber: string;
  password: string;
  departmentId: number;
  departmentName?: string;
  role: UserRole;
  isActive: boolean;
}
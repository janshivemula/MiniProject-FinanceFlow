export interface ILoginRequest {
  email: string;
  password: string;
}

export interface ILoginResponse {
  employeeId: number;
  employeeName: string;
  email: string;
  role: number;
  departmentId: number;
  departmentName: string;
}
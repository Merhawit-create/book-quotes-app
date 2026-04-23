export interface AuthResponseModel {
  isSuccess: boolean;
  userName: string;
  token: string;
  errors: string[];
}

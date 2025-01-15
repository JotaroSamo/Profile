export interface CreateUser {
    login: string;
    firstName: string;
    lastName: string;
    avatarUrl?: string;
    password: string;
    confirmPassword: string;
  }
  
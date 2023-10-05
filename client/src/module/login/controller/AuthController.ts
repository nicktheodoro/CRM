import { AuthenticateUseCase } from "../domain/useCase/authenticateUseCase";

interface AuthenticateRequest {
  email?: string;
  password?: string;
}

class AuthenticateController {
  public form: AuthenticateRequest = {
    email: undefined,
    password: undefined,
  };
  public showPassword = false;

  constructor(
    private context: any,
    private authenticateUseCase: AuthenticateUseCase
  ) {
    console.log(authenticateUseCase)
  }

  async signIn() {
    await this.authenticateUseCase(this.form);
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}

export { AuthenticateController };

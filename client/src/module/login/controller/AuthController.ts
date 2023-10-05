import { Store } from "vuex";
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
    private store: Store<any>,
    private authenticateUseCase: AuthenticateUseCase
  ) {}

  async signIn() {
    await this.store.dispatch(
      "authenticate",
      this.authenticateUseCase(this.form)
    );
      
    if (this.store.state.isAuthenticated) {
      this.context.$router.push("/dashboard");
    } else {
      alert("Email ou senha incorretos.")
    }
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}

export { AuthenticateController };

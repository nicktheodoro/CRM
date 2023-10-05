import { headers } from "../const/tableHeaders";
import { UserModel, UserPagination } from "../domain/model/user";
import { FetchUsersUseCase } from "../domain/useCase/fetchUsersUseCase";
import { CreateUserUseCase } from "../domain/useCase/createUserUseCase";
import { AuthenticateUseCase } from "@/module/login/domain/useCase/authenticateUseCase";
import { Store } from "vuex";

class UserController {
  public options: any;
  public user = new UserModel({});
  public userPagination = new UserPagination();
  public headers = headers;

  constructor(
    private context: any,
    private store: Store<any>,
    private fetchUsersUseCase: FetchUsersUseCase,
    private createUserUseCase: CreateUserUseCase,
    private authenticateUseCase: AuthenticateUseCase
  ) {}

  async paginate() {
    this.userPagination = await this.fetchUsersUseCase(this.options, "");
  }

  async signUp() {
    if (this.context.$refs.UserForm.$refs.form.validate()) {
      const user = await this.createUserUseCase(this.user);

      if (user.email && user.password) {
        await this.store.dispatch(
          "authenticate",
          this.authenticateUseCase({
            email: user.email,
            password: user.password,
          })
        );
      }

      // this.cancel();

      this.context.$router.push("/dashboard");
    }
  }

  cancel() {
    this.user = new UserModel({});
    this.context.$refs.UserForm.$refs.form.resetValidation();
  }
}

export { UserController };

import { headers } from "../const/tableHeaders";
import { UserModel, UserPagination } from "../domain/model/user";
import { FetchUsersUseCase } from "../domain/useCase/fetchUsersUseCase";
import { CreateUserUseCase } from "../domain/useCase/createUserUseCase";

class UserController {
  public options: any;
  public formDialog = false;
  public user = new UserModel({});
  public userPagination = new UserPagination();
  public headers = headers;

  constructor(
    private context: any,
    private fetchUsersUseCase: FetchUsersUseCase,
    private createUserUseCase: CreateUserUseCase
  ) {}

  async paginate() {
    this.userPagination = await this.fetchUsersUseCase(this.options, "");
  }

  async save() {
    if (this.context.$refs.UserForm.$refs.form.validate()) {
      await this.createUserUseCase(this.user);
      this.cancel();
      this.paginate();
    }
  }

  cancel() {
    this.user = new UserModel({});
    this.context.$refs.UserForm.$refs.form.resetValidation();
    this.formDialog = false;
  }
}

export { UserController };

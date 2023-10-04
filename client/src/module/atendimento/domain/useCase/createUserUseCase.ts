import { CreateUserRepository } from "../../repository/createUserRepository";
import { UserModel } from "../model/user";

interface CreateUserUseCase {
  (product: UserModel): Promise<UserModel>;
}

const createUserUseCase =
  (repository: CreateUserRepository): CreateUserUseCase =>
  async (user: UserModel) => {
    const created = await repository(user);
    return created;
  };

export { createUserUseCase, CreateUserUseCase };

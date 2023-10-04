import { AxiosInstance } from "axios";

import { UserModel } from "../domain/model/user";

interface CreateUserRepository {
  (user: UserModel): Promise<UserModel>;
}

const createUserRepository =
  (axios: AxiosInstance): CreateUserRepository =>
  async (user: UserModel) => {
    const response = await axios.post("/users", user);
    return new UserModel(response?.data);
  };

export { createUserRepository, CreateUserRepository };

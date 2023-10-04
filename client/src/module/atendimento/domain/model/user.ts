import { AxiosResponse } from "axios";

interface UserI {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  isActive?: boolean | null;
  password?: boolean | null;
}

class UserModel {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  isActive?: boolean | null;
  password?: boolean | null;

  constructor(data: UserI) {
    this.id = data.id ?? null;
    this.name = data.name ?? null;
    this.email = data.email ?? null;
    this.isActive = data.isActive ?? null;
    this.password = data.password ?? null;
  }
}

class UserPagination {
  page: number;
  pageSize: number;
  items: UserI[];
  count: number;

  constructor(response?: AxiosResponse) {
    this.page = response?.data?.page ?? 0;
    this.pageSize = response?.data?.pageSize ?? 0;
    this.items =
      response?.data?.items?.map((u: UserI) => new UserModel(u)) ?? [];
    this.count = response?.data?.count ?? 0;
  }
}

export { UserModel, UserPagination };

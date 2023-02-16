const baseUrl = "/api/user";

export const getAllUsers = () => {
  return fetch(baseUrl).then((res) => res.json());
};

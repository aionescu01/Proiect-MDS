import axios from "axios";
import { json } from "body-parser";
const API_URL = "https://localhost:44307/api/Auth/";
class AuthService {
  login(email, password) {
    return axios
      .post(API_URL + "login", {
        email,
        password
      })
      .then(response => {
        if (response.data.accessToken) {
          localStorage.setItem("user", JSON.stringify(response.data));
          localStorage.setItem("jwt",JSON.stringify(response.data.accessToken).replaceAll("\"",""))
          localStorage.setItem("refreshToken",JSON.stringify(response.data.refreshToken))
        } 
        return response.data;
      });
  }
  logout() {
    localStorage.removeItem("user");
  }
  register(firstName, lastName, email, password,confirmPassword) {
    return axios.post(API_URL + "register", {
      firstName,
      lastName,
      email,
      password,
      confirmPassword
    });
  }
  getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));;
  }
}
export default new AuthService();

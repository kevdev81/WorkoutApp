import React from "react";
import { handlePostRegisterUser } from "./registerService";
import { Alert } from "reactstrap";
import RegisterForm from "./RegisterForm";

class Register extends React.Component {
  state = {
    email: "",
    firstName: "",
    lastName: "",
    password: "",
    passwordConfirm: ""
  };

  handleInputChange = e => {
    const target = e.target;
    const value = target.value;
    const name = target.name;

    this.setState({
      [name]: value
    });
  };

  // Need to finish validation in front
  registerUser = () => {
    const formData = {
      email: this.state.email,
      firstName: this.state.firstName,
      lastName: this.state.lastName,
      password: this.state.password,
      passwordConfirm: this.state.passwordConfirm
    };
    handlePostRegisterUser(formData)
      .then(this.onRegisterUserSuccess)
      .catch(this.onRegisterUserError);
  };
  onRegisterUserSuccess = () => {
    //delete log
    this.props.history.push("/");
    console.log("User has been successfully registered.");
  };
  onRegisterUserError = () => {
    //delete log
    alert("There was an error creating your account.");
  };

  goToLoginPage = () => {
    this.props.history.push("/");
  };

  render() {
    const {
      email,
      firstName,
      lastName,
      password,
      passwordConfirm
    } = this.state;
    const { handleInputChange, registerUser, goToLoginPage } = this;
    return (
      <RegisterForm
        handleInputChange={handleInputChange}
        registerUser={registerUser}
        goToLoginPage={goToLoginPage}
        email={email}
        firstName={firstName}
        lastName={lastName}
        password={password}
        passwordConfirm={passwordConfirm}
      />
    );
  }
}

export default Register;

<template>
  <div class="login">
    <div class="header">
      Logowanie
    </div>
    <div class="wrapper">
      <div class="label">
        Email:
      </div>
      <input ref="loginText"/>

    </div>
    <div class="wrapper">
      <div class="label">
        Hasło:
      </div>
      <input type="password" ref="passwordText"/>

    </div>
    <div class="submit-button" @click="login()">
      Zaloguj
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  methods: {
    login() {
      let emailTextBox = this.$refs.loginText
      let passwordTextBox = this.$refs.passwordText
      this.$store.dispatch('main/userLogin', {emailText: emailTextBox.value, passwordText: passwordTextBox.value})
    }
  }
}
</script>

<style scoped lang="scss">
.login {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 30px;
  background-color: rgba(255, 255, 255, 0.1);

  .header {
    width: 100%;
    height: 40px;
    font-size: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .header, .label {
    border: solid 2px var(--border-color-1);
    background-color: var(--bg-block-color);
  }

  .wrapper {
    width: 600px;
    display: flex;
    margin: 30px 0;
    height: 50px;

    .label {
      flex: 1;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 50px;
      margin-right: 20px;
    }

    textarea {
      flex: 4;
      word-wrap: break-word;
      word-break: break-all;
      background-color: var(--bg-color);
      resize: none;
      padding: 10px;
    }

  }

  .submit-button {
    position: fixed;
    bottom: 40px;
    right: 40px;
    color: var(--acefb9);
    border: solid 4px var(--border-color-1);
    width: 200px;
    height: 70px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-weight: bold;
    font-size: 25px;
    background-color: rgba(172, 239, 185, 0.1);
    cursor: pointer;
    transition: all .2s ease-in-out;

    &:hover {
      background-color: rgba(172, 239, 185, 0.15);
    }
  }
}
</style>
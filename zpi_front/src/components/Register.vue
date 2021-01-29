<template>
  <div class="register">
    <div class="header">
      Rejestracja
    </div>

    <div class="wrapper">
      <div class="label">
        Imię:
      </div>
      <input ref="nameText"/>
    </div>
    <div class="wrapper">
      <div class="label">
        Nazwisko:
      </div>
      <input ref="surnameText"/>
    </div>
    <div class="wrapper">
      <div class="label">
        Email:
      </div>
      <input ref="emailText"/>
    </div>
    <div class="wrapper">
      <div class="label">
        Hasło:
      </div>
      <input type="password" ref="passwordText" class="password"/>
    </div>
    <div class="account-type wrapper">
      <div class="label">
        Typ konta:
      </div>
      <v-select v-model="selected" :options="accountTypeList" label="name" class="style-chooser"/>
    </div>
    <div class="wrapper">
      <div class="label">
        Index:
      </div>
      <input ref="indexText"/>
    </div>
    <div class="submit-button" @click="register()">
      Zarejestruj
    </div>
  </div>
</template>

<script>
import vSelect from 'vue-select';

export default {
  name: "Register",
  data() {
    return {
      selected: null
    }
  },
  components: {
    vSelect
  },
  computed: {
    accountTypeList() {
      return [
        {
          id: 1,
          name: 'Student',
          type: "stud",
        },
        {
          id: 2,
          name: 'Pracownik uczelni',
          type: "work",
        }]
    }
  },
  methods: {
    register() {
      let nameTextBox = this.$refs.nameText
      let surnameTextBox = this.$refs.surnameText
      let emailTextBox = this.$refs.emailText
      let passwordTextBox = this.$refs.passwordText
      let accountType = this.selected.type
      let indexTextBox = this.$refs.indexText
      this.$store.dispatch('main/userRegister',
          {
            nameText: nameTextBox.value,
            surnameText: surnameTextBox.value,
            emailText: emailTextBox.value,
            passwordText: passwordTextBox.value,
            accountText: accountType,
            indexText: indexTextBox.value,
          })
    }
  }
}
</script>

<style scoped lang="scss">
.register {
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
      border: solid 1px var(--border-color-1);

      &:focus {
        outline: none !important;
        border: 1px solid var(--acefb9);
        box-shadow: 0 0 5px var(--acefb9);
      }
    }

    .v-select {
      flex: 4;
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
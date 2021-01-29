<template>
  <div class="header" >
    <img src="../assets/logo_zpi.jpg" class="logo" @click="$router.push('/')" alt="logo"/>
    <div class="header-buttons">
      <div v-if="userLoggedIn">
        Wiadomości
      </div>
      <div v-if="userLoggedIn" @click="openTeams" :class="teamsSelected ? 'selected' : '' ">
        Zespoły
      </div>
      <div v-if="userLoggedIn" @click="openTopics" :class="topicsSelected ? 'selected' : '' ">
        Tematy
      </div>
      <div v-if="userLoggedIn" @click="openMyTeam" :class="myTeamSelected ? 'selected' : '' ">
        Mój zespół
      </div>
      <div v-if="userLoggedIn">
        Moje konto
      </div>
      <div class="login" @click="loginMethod" :class="loginSelected ? 'selected' : '' ">
        <p v-if="!userLoggedIn">
          Zaloguj
        </p>
        <p v-else>
          Wyloguj
        </p>
      </div>
      <div class="register" v-if="!userLoggedIn" @click="registerMethod" :class="registerSelected ? 'selected' : '' ">
        Rejestracja
      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: "Header",
  computed: {
    userLoggedIn() {
      return this.$store.state.main.userLoggedIn;
    },
    teamsSelected() {
      return this.$route.name === 'Teams'
    },
    topicsSelected() {
      return this.$route.name === 'Topics'
    },
    myTeamSelected() {
      return this.$route.name === 'MyTeam'
    },
    loginSelected(){
      return this.$route.name === 'Login'
    },
    registerSelected(){
      return this.$route.name === 'Register'
    }

  },
  methods: {
    loginMethod() {
      this.$router.push('/login')
    },
    registerMethod() {
      this.$router.push('/register')
    },
    openTeams() {
      this.$router.push('/teams')
    },
    openTopics() {
      this.$router.push('/topics')
    },
    openMyTeam() {
      this.$router.push('/my-team')
    }
  }
}
</script>

<style scoped lang="scss">
.header {
  width: 100%;
  height: 110px;
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  padding: 20px 0;
  justify-content: space-between;
  background-color: #161b22;
  color: white;

  .logo {
    height: 100%;
    margin-left: 50px;
    border-radius: 15px;
    cursor: pointer;
  }

  .header-buttons {
    display: flex;
    height: 100%;
    align-items: center;
    margin-right: 50px;

    div {
      display: flex;
      padding: 0 20px;
      align-items: center;
      justify-content: center;
      height: 50%;
      margin-left: 20px;
      font-size: 20px;
      cursor: pointer;
      transition: all ease-in-out .2s;
      border-radius: 10px;

      &:hover, &.selected {
        background-color: #727272;
      }

      &.login, &.register {
        color: #acefb9;
        font-weight: bold;
        cursor: pointer;
      }
    }
  }

}
</style>
<template>
  <div class="header">
    <img src="../assets/logo_zpi.jpg" class="logo"/>
    <div class="header-buttons">
      <div>
        DZIKIE ŚLIWKI
      </div>
      <div>
        Kursy
      </div>
      <div>
        Zespoły
      </div>
      <div>
        Zakładki
      </div>
      <div class="login">
        Zaloguj
      </div>
    </div>
  </div>

</template>

<script>
export default {
  name: "Header"
}
</script>

<style scoped lang="scss">
.header {
  width: 100%;
  height: 110px;
  display: flex;
  align-items: center;
  margin: 20px 0;
  padding-bottom: 20px;
  justify-content: space-between;
  border-bottom: 1px solid black;


  .logo {
    height: 100%;
    margin-left: 50px;
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

      &.login {
        color: #42b983;
        font-weight: bold;
      }
    }
  }

}
</style>
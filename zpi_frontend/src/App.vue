<template>
    <div class="page">
      <Header/>
      <div class="page-wrapper">
        <router-view/>
      </div>
    </div>
</template>

<script>

import Header from "@/components/Header";


export default {
  name: 'App',
  components: {Header}
}
</script>


<style>
@font-face {
  font-family: Roboto;
  src: url('./fonts/Roboto-Regular.ttf');
  font-style: normal;
  font-weight: normal;
}

@font-face {
  font-family: Roboto;
  src: url('./fonts/Roboto-Bold.ttf');
  font-style: normal;
  font-weight: bold;
}

@font-face {
  font-family: Roboto;
  src: url('./fonts/Roboto-Bold.ttf');
  font-style: normal;
  font-weight: 500;
}

@font-face {
  font-family: Roboto;
  src: url('./fonts/Roboto-Light.ttf');
  font-style: normal;
  font-weight: 300;
}

html, body, .page {
  width: 100%;
  margin: 0;
  padding: 0;
  font-family: Roboto, serif !important;
  font-weight: 400;
  font-size: 16px;
  font-stretch: normal;
  font-style: normal;
  line-height: normal;
  letter-spacing: normal;
  color: #000000;
}

body {
  min-width: 320px;
  overflow-x: hidden;
}

html, body {
  width: 100%;
  height: 100%;
}

* {
  box-sizing: border-box;
}

b, strong, .bold {
  font-weight: bold;
}
</style>

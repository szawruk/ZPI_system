<template>
  <div class="page">
    <Header/>
    <div class="page-wrapper" :style="cssVars">
      <router-view/>
    </div>
  </div>
</template>

<script>

import Header from "@/components/Header";

export default {
  name: 'App',
  components: {Header},
  computed: {
    cssVars() {
      return {
        '--bg-block-color': '#131822',
        '--bg-block-hover': '#1e2435',
        '--bg-color': '#0d1117',
        '--border-color-1': '#30363d',
        '--border-color-2': '#777676',
        '--acefb9': '#acefb9',
      }
    }
  }

}
</script>


<style lang="scss">

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

html, body, .page, textarea {
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
  color: white;
}

body {
  min-width: 320px;
  overflow-x: hidden;
}

html, body {
  width: 100%;
  height: 100%;
  background-color: #0d1117;
}

* {
  box-sizing: border-box;
}

b, strong, .bold {
  font-weight: bold;
}

.page-wrapper {
  max-width: 1350px;
  margin: 0 auto;

}

.vs__dropdown-toggle {
  height: 50px;
  color: white;
  background-color: white;
}

.style-chooser .vs__search::placeholder,
.style-chooser .vs__dropdown-toggle,
.style-chooser .vs__dropdown-menu {
  background-color: var(--bg-color);
}
.style-chooser{
  .vs__dropdown-menu{
    outline: none !important;
    border: 2px solid var(--acefb9);
    border-top: none;
    box-shadow: 0 0 15px var(--acefb9);
  }
}
.style-chooser{
  .vs__dropdown-option{
    background-color: transparent;
    &:hover{
      background-color: var(--acefb9);
      color: black;
    }
  }
}

.style-chooser{
  &.vs--open{
    outline: none !important;
    border: 1px solid var(--acefb9);
    box-shadow: 0 0 5px var(--acefb9);
  }
}

input {
  color: white;
  flex: 4;
  word-wrap: break-word;
  word-break: break-all;
  background-color: var(--bg-color);
  resize: none;
  padding: 10px;
  outline: none;
  border: none;
  font-size: 16px;

  &:focus {
    outline: none !important;
    border: 1px solid var(--acefb9);
    box-shadow: 0 0 5px var(--acefb9);
  }
}

textarea{
  border: none;
}
</style>

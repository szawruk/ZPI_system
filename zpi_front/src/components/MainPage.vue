<template>
    <div class="main-page">
        <div class="container">
           <p>
             Witamy na stronie ZPI!
           </p>
          <p>
            Szymon Awruk,  Adam Sierakowski
          </p>


        </div>
    </div>
</template>

<script>
    export default {
        name: 'MainPage'
    }</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .main-page {
        width: 100%;
        display: flex;
        justify-content: flex-start;
      margin:30px 50px 0;
    
    }
    .container {
      color: #999999;
      font-weight: bold;
      font-size: 25px;
    }
</style>

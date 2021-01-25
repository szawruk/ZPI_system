<template>
  <div class="teams" >
    <add-new :method="() =>this.$router.push('/teams/new')"/>
    <div class="team" v-for="team in teamsList" :key="team.team">
      <div class="team-image">
        <img src="../assets/team.png" alt="team"/>
      </div>
      <div class="team-content">
        <div class="team-header">
          <div class="team-name">
            {{ team.name }}
          </div>
          <div class="team-topic">
            {{ team.topic.name }}
          </div>
        </div>

        <div class="team-description">
          {{ team.description }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import AddNew from "@/components/controls/AddNew";

export default {
  name: "Teams",
  components:{
    AddNew
  },
  computed: {
    teamsList() {
      return this.$store.state.teams.teamsList
    }
  },
mounted() {
  this.$store.dispatch('teams/getTeamsList')
}
}
</script>

<style scoped lang="scss">
.teams {
  width: 100%;
  margin-top: 30px;

  .team {
    height: 150px;
    border: solid 2px var(--border-color-1);
    padding: 20px;
    margin-bottom: 30px;
    display: flex;
    background-color: var(--bg-block-color);

    .team-image {
      flex: 2;
      display: flex;
      justify-content: center;
      align-items: center;

      img {
        height: 80px;
        object-fit: cover;
        filter: invert(96%) sepia(10%) saturate(1236%) hue-rotate(61deg) brightness(99%) contrast(88%);
      }
    }

    .team-content {
      flex: 14;
      margin-left: 30px;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      padding: 5px;
      color: white;

      .team-header {
        display: flex;

        div{
          display: flex;
          justify-content: center;
          border-bottom: solid 1px var(--border-color-2);
        }

        .team-name {
          flex: 1;
          padding: 10px;
          margin-right: 100px;
        }

        .team-topic {
          flex: 3;
          padding: 10px;
        }
      }


      .team-description {
        width: 100%;
        padding: 10px;
        border-bottom: solid 1px #777676;
      }


    }
  }

}
</style>
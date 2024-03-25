pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'Initiating build process with Maven...'
            }
        }
        
        stage('Unit and Integration Tests') {
            steps {
                echo 'Executing unit tests...'
                echo 'Conducting integration tests...'
            }
        }
        
        stage('Code Analysis') {
            steps {
                echo 'Performing code analysis via SonarQube...'
            }
        }
        
        stage('Security Scan') {
            steps {
                echo 'Conducting security evaluation using OWASP ZAP...'
            }
        }
        
        stage('Deploy to Staging') {
            steps {
                echo 'Deploying application to staging environment (e.g., AWS EC2 instance)...'
            }
        }
        
        stage('Integration Tests on Staging') {
            steps {
                echo 'Executing integration tests on staging environment...'
            }
        }
        
        stage('Deploy to Production') {
            steps {
                echo 'Deploying application to production server (e.g., AWS EC2 instance)...'
            }
        }
    }
    post {
        success {
            emailext subject: "Pipeline '${currentBuild.fullDisplayName}' Succeeded",
                      body: 'Congratulations! The build was successful.',
                      to: 'anshpreetkathpal1234@gmail.com',
                      attachLog: true
        }
        failure {
            emailext subject: "Pipeline '${currentBuild.fullDisplayName}' Failed",
                      body: 'Attention: The build has failed. Please investigate.',
                      to: 'anshpreetkathpal1234@gmail.com',
                      attachLog: true
        }
    }
}

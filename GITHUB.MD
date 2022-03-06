# Git

## What is it?
Git is a version control system that allows multiple developers to contribute to a project simultaneously. It is a command-line application with a set of commands to manipulate commits and branches.  Git uses a database, located in a `.git` folder, to track changes.

Git tracks changes to your files in three states:
* *Modified* means that you have changed the file but have not committed it to your database yet.
* *Staged* means that you have marked a modified file in its current version to go into your next commit snapshot.
* *Committed* means that the data is safely stored in your local database.

![Git tracks changes to your files in three states](https://git-scm.com/book/en/v2/images/areas.png)

### Installation
* [Install git for windows](https://gitforwindows.org/)

#### Blame Information
Set your user name and email address. This is important because every Git commit uses this information identity commits:

`git config --global user.name "John Doe"`
`git config --global user.email johndoe@example.com`

### Github 

GitHub offers the distributed version control and source code management (SCM) functionality of Git. It provides access control and several collaboration features such as bug tracking, feature requests, task management, continuous integration and wikis for every project.  Github will serve as your remote.

*Github is a distributed version control system*

![Github is a distributed version control system](https://git-scm.com/book/en/v2/images/distributed.png)

#### Github Collaborators
For teamwork, it is only necessary to create one repository. One of you will invite the others to this repository as [collaborators](https://docs.github.com/en/account-and-profile/setting-up-and-managing-your-github-user-account/managing-access-to-your-personal-repositories/inviting-collaborators-to-a-personal-repository).

## Recommended Git Workflow

1. Create local and remote repositories
2. Use branches to separate features and tasks
3. Pull changes from the remote repository
4. Work on coding tasks
5. Track changes using commits
6. Push commits to the remote repository

### Pre-requisites
* `git` is installed
* Use of a terminal (PowerShell, Git-Bash, or the Terminal in Mac)
* Version 6.0 of the dotnet SDK is installed. [Download]
    * confirmation: run this command `dotnet info` or this one `dotnet --list-sdks` from the terminal
    * if any earlier versions of the SDK exist, [uninstall them](https://docs.microsoft.com/en-us/dotnet/core/install/remove-runtime-sdk-versions?pivots=os-windows)
* [Visual Studo Code](https://code.visualstudio.com/)

### Create local and remote repositories
1. *Start a new repository at Github*
    1. Do not add any files (do not check any checkboxes offering READMEs or licenses)
    2. Take note of the link to the repository ![Repository Link](https://i.imgur.com/YUqXwhn.png)
2. On your local machine (in your filesystem), create a folder where you want your project to be created
3. use the `pwd` command to make sure that you are in the place where your project will be created and tracked by git.
4. run `git init` from the terminal where your project will be created - [what does this command do?](https://git-scm.com/docs/git-init)
5. run `git status` from the terminal to confirm that you have a local repository started - [what does this command do?](https://git-scm.com/docs/git-status)
6. Add a README.MD file: `New-Item README.MD`
7. Use this README.MD file to document your project.  Here is a guide for using [Markdown](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)
8. *Add remote reference:* `git remote add origin https://github.com/<username>/<remote_repository>.git`
9. *Add files for tracking:* `git add -A`
10. *Commit changes:* `git commit -m '<commit message>'` - the `<commit message>` should be a brief descrtive phrase that describes the nature of the changes in the commit.
11. *Push to remote*: `git push -u origin master`

Reference material: [Creating a Git Repository](https://git-scm.com/book/en/v2/Git-Basics-Getting-a-Git-Repository)

### What if the remote already exists?
This is common.  If this is the case, you will clone the existing project:
1. use the `pwd` command to make sure that you are in the place where your project will be created and tracked by git.
2. clone the existing project: `git clone https://github.com/libgit2/libgit2`
3. ensure remote is created, as above

### Pull from the Remote
1. When you first sit down to work, pull changes from the remote: `git pull origin <branch-name>`
2. `<branch name>` refers to the branch you'd like to pull down for editing

### Commit Procedures
Do these steps everytime you want to commit changes to the local repository:
1. *Add files for tracking:* `git add -A`
2. *Commit changes:* `git commit -m '<commit message>'` - the `<commit message>` should be a brief descrtive phrase that describes the nature of the changes in the commit.

### Push to the remote
1. When you are a a breaking point, or want your local changes to be available at the remote, push changes to the remote: `git push origin <branch-name>`
2. `<branch name>` refers to the branch you'd like to pull down for editing

# Git Topics
These are the operations you will commonly use:
* Tracking files
* Commiting changes
* Undoing changes
* Working with Remotes
* Tagging
* Branching and Merging
* Branch Management and Workflows
* Remote Branches
* Rebasing

## Tracking Files
* *ignorining files*: There are files that you don’t want Git to bother with. The `.gitignore` file is used to create patterns of files to be ignored.
    * use the dotnet tool to create a gitignore file for dotnet projects: `dotnet new gitignore`
* *adding files for tracking:* `git add -A`
* *current status of repository:* `git status`

## Commiting Changes
* *commiting changes:* `git commit -m '<feature|task id|name>: <description>'`
    * *feature|task id|name* from AzureDevOps
    * *description* short phrase describing the reason for commiting these changes
* *list of changes:* `git log --pretty=oneline`

## Undoing Changes
* *removing a file from being tracked:* `git rm --cached <filename>`
* *add a forgotten change:* `git commit --amend`
* *Unstaging/resetting a change:* `git restore --staged <filename>`
* *Unmodifying a committed change*:  `git restore <filename>`

## Working with Remotes
* *show current remotes:* `git remote -v`
* *add a remote:* `git remote add <alias> <url>`
    * The `alias` is any name you'd care to use - most tutorials use *origin*
* *get remote changes:* `git fetch <remote> <branch>`
* *get remote changes and merge:* `git pull <remote> <branch>` *you will mostly use this*

### Git Pull
`git pull` incorporates changes from a remote repository into the current branch. If the current branch is behind the remote, then by default it will fast-forward the current branch to match the remote. If the current branch and the remote have diverged, the user needs to specify how to reconcile the divergent branches with --rebase or --no-rebase (or the corresponding configuration option in pull.rebase). `git pull` runs `git fetch` and will then call either `git rebase` or `git merge` to reconcile diverging branches.

## Tagging
Git allows for tagging specific points in a repository’s history. This can be used to mark release points, sprints, or other milestones. 

* *list current tags:* `git tag`
* *create tag:* `git tag -a <annotation> -m "<description"`
    * *annotation* - the label to use
* *after-the-fact tagging:* `git tag -a <annotation> <checksum>`
    * *checksum* - all commits have a unique hased fingerprint

## Branching and Merging
A branch is an independent pointer to the change history of the git database.  All commits to the database are tracked using a uniqe hash/checksum.  The list of these commits is how git tracks all changes.

### Commit history

![Commit history](https://git-scm.com/book/en/v2/images/commits-and-parents.png)

There is a unique "pointer" to the list these changes called the **HEAD**.

### HEAD pointer

![HEAD pointer](https://git-scm.com/book/en/v2/images/branch-and-history.png)

We get a master branch out of the box with Git, but we can create other branches.

### Master and testing branch example

![Master and testing branch example](https://git-scm.com/book/en/v2/images/two-branches.png)

We move the HEAD pointer around to change between branches.

### Tracking branches with the HEAD pointer

![Tracking branches with the HEAD pointer](https://git-scm.com/book/en/v2/images/head-to-master.png)

### Switching branches with the HEAD pointer

![Switching branches with the HEAD pointer](https://git-scm.com/book/en/v2/images/head-to-testing.png)

### On the Testing branch

![On the Testing branch](https://git-scm.com/book/en/v2/images/advance-testing.png)

### Branch Divergence

![Branch Divergence](https://git-scm.com/book/en/v2/images/advance-master.png)

## Basic Branching and Merging
Here is an example branching and merging workflow. You’ll follow these steps:

* Given a feature for the application.
* Create a branch for feature/user story you’re working on.
* Do some task work in that branch.
* Merge the changes downwards to a development/production/master branch.

### Create a Branch

`git checkout -b <branch-name>`

![Create a Branch](https://git-scm.com/book/en/v2/images/basic-branching-2.png)


### Commit Changes to New Branch

`git commit -a -m 'commit message <feature|task id|name>'`

![Commit Changes to New Branch](https://git-scm.com/book/en/v2/images/basic-branching-3.png)

### Merging From and To
`git checkout master`

`git merge <feature-branch>`

![Merging From and To](https://git-scm.com/book/en/v2/images/basic-merging-1.png)

### After the Merge 

![After the Merge ](https://git-scm.com/book/en/v2/images/basic-merging-2.png)

#### Optionally: Deleting the Branch

`git branch -d <feature-branch>`

![Optionally: Deleting the Branch](https://git-scm.com/book/en/v2/images/basic-merging-2.png)

## Merge Conflicts
You will inevitably encounter conficts along the way. These happen when you have different changes/content in same file in the two branches you’re merging.  In these cases, Git won’t be able to merge them cleanly.  You will get a message like this:

```
$ git merge iss53
Auto-merging index.html
CONFLICT (content): Merge conflict in index.html
Automatic merge failed; fix conflicts and then commit the result.
```

The file contents will look something like this:

![merge conflict](https://i.imgur.com/P9NCJ3v.png)

It is common that some visual merging tool is available with your install of git.  Or, VS Code will assist (as is the case in the above picture).

`git mergetool`

# Branching Workflows

## Typical Layout

![Typical Branch History](https://git-scm.com/book/en/v2/images/lr-branches-1.png)

![Typical Branch Parallel History](https://git-scm.com/book/en/v2/images/lr-branches-2.png)

## Branches for Separation

![Multiple topic branches](https://git-scm.com/book/en/v2/images/topic-branches-1.png)

## Downstream Resolution

![History after merging](https://git-scm.com/book/en/v2/images/topic-branches-2.png)

## Use this Branching Approach
Adopt the following branch strategy:
* *main branch* - overall version
* *sprint branch* - branch to organize new features per branch
* *feature branch* - branch to organize the changes required to implement a feature
* *task branch* - branch to organize task and implementations for a feature

*Recommended branching strategy*

![Recommended branching strategy](https://i.imgur.com/vp73oO8.png)

NOTE: Individual developers are assigned to tasks.

# Distributed Workflows

NOTE: This is very applicable to teams in CIDM4390

## Using Github as a Shared Repository

![Using Github as a Shared Repository](https://git-scm.com/book/en/v2/images/centralized_workflow.png)

There are a number of collaboration strategies in a distributed workflow that you may consider.

## Adopting an Integration-Manager Workflow

Somebody on the team, perhaps the Scrum Master, acts as the "Integration Manager."  The process proceeds accordingly:

* The Scrum Master (project maintainer) pushes to their public repository.
* A contributor clones that repository and makes changes.
* The contributor pushes to their own public copy.
* The contributor sends the maintainer a [pull request](https://www.git-scm.com/docs/git-request-pull) asking them to pull changes. 
    * [About Pull Requests](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests).
    * [Github: Creating a pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).
* The maintainer adds the contributor’s repository as a remote and merges locally.
* The maintainer pushes merged changes to the main repository.

## Adopting the Benevolent Dictator Workflow

This is a variant of a multiple-repository Integration-Manager Workflow. A "lieutenant" integration managers is in charge of certain parts of the repository. Above his, all the lieutenants have one integration manager known as the benevolent dictator. The benevolent dictator pushes from their directory to a reference repository from which all the collaborators need to pull.  The control aspects of this may even be attractive for a small team where the "lieutenant" role is skipped and only the benevolent dictator remains."

![Benevolent Dictator Workflow](https://git-scm.com/book/en/v2/images/benevolent-dictator.png)